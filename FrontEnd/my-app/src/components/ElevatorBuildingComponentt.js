import { useEffect, useRef, useState } from "react";
import {
  Box,
  Card,
  CardContent,
  Grid,
  Paper,
  Typography,
  List,
  ListItem,
  ListItemText,
} from "@mui/material";
import Elevator from "./Elevator";
import Building from "./Building";
import { createElevatorCall } from "./servers/ElevatorCallService";
import * as signalR from "@microsoft/signalr";

export default function ElevatorBuildingComponent({
  props,
  moveInterval = 600,
}) {
  const [currentFloor, setCurrentFloor] = useState(1);
  const [targetFloor, setTargetFloor] = useState(null);
  const [queue, setQueue] = useState([]);
  const [direction, setDirection] = useState(null);
  const [statusMessage, setStatusMessage] = useState("idle");
  const [phase, setPhase] = useState("idle");

  const timerRef = useRef(null);
  const stopTimeoutRef = useRef(null);
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7229/elevatorHub")
      .withAutomaticReconnect()
      .build();
    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (!connection) return;
    let isMounted = true;

    async function startConnection() {
      try {
        await connection.start();
        console.log("Connected to ElevatorHub");

        const initialStatus = await fetch(
          `https://localhost:7229/api/elevator/status?buildingId=${props.id}`
        ).then((res) => res.json());

        if (isMounted && initialStatus) {
          setCurrentFloor(initialStatus.currentFloor);
          setQueue(initialStatus.queue || []);
          setDirection(initialStatus.direction || null);
          setStatusMessage(initialStatus.statusMessage || "idle");
        }

        connection.on("ReceiveElevatorUpdate", (status) => {
          if (!isMounted) return;
          setCurrentFloor(status.currentFloor);
          setQueue(status.queue || []);
          setDirection(status.direction || null);
          setStatusMessage(status.statusMessage || "idle");
          setTargetFloor(status.targetFloor || null);
          setPhase(status.phase || "idle");
        });

      } catch (err) {
        console.error("Error connecting to ElevatorHub:", err);
      }
    }

    startConnection();

    return () => {
      isMounted = false;
      connection.stop();
    };
  }, [connection, props.id]);

  async function pushRequest(f) {
    if (f === currentFloor && phase === "idle") {
      setPhase("doorOpening");
      setStatusMessage("doorOpening");
      return;
    }

    setQueue((q) => {
      if (q.includes(f) || (f === targetFloor && phase === "moving")) return q;
      return [...q, f];
    });

    try {
      await createElevatorCall({
        buildingId: props.id,
        requestedFloor: currentFloor,
        destinationFloor: f,
        callTime: new Date().toISOString(),
        isHandled: false,
      });
    } catch (err) {
      console.error("Error sending elevator call:", err);
    }
  }

  function getNextFloor() {
    if (queue.length === 0) return null;
    if (direction === null) {
      setDirection(queue[0] > currentFloor ? "up" : "down");
    }
    if (direction === "up") {
      const ups = queue.filter((f) => f > currentFloor).sort((a, b) => a - b);
      if (ups.length > 0) return ups[0];
      const downs = queue.filter((f) => f < currentFloor).sort((a, b) => b - a);
      if (downs.length > 0) {
        setDirection("down");
        return downs[0];
      }
    }
    if (direction === "down") {
      const downs = queue.filter((f) => f < currentFloor).sort((a, b) => b - a);
      if (downs.length > 0) return downs[0];
      const ups = queue.filter((f) => f > currentFloor).sort((a, b) => a - b);
      if (ups.length > 0) {
        setDirection("up");
        return ups[0];
      }
    }
    return null;
  }

  useEffect(() => {
    if (phase === "idle" && queue.length > 0) {
      let next = getNextFloor();
      if (next == null) {
        next = queue[0];
      }
      setPhase("moving");
      setTargetFloor(next);
      setStatusMessage(`moving -> ${next}`);
    }
  }, [phase, queue]);

  useEffect(() => {
    if (phase === "moving" && targetFloor != null) {
      if (timerRef.current) clearInterval(timerRef.current);
      timerRef.current = setInterval(() => {
        setCurrentFloor((cf) => {
          if (cf === targetFloor) {
            clearInterval(timerRef.current);
            timerRef.current = null;
            setQueue((q) => q.filter((f) => f !== cf));
            setPhase("doorOpening");
            setStatusMessage("doorOpening");
            return cf;
          }
          return cf < targetFloor ? cf + 1 : cf - 1;
        });
      }, moveInterval);
    }
    return () => {
      if (timerRef.current) clearInterval(timerRef.current);
    };
  }, [phase, targetFloor, moveInterval]);

  useEffect(() => {
    if (phase === "doorOpening") {
      stopTimeoutRef.current = setTimeout(() => {
        setPhase("doorClosing");
        setStatusMessage("doorClosing");
      }, 1000);
    } else if (phase === "doorClosing") {
      stopTimeoutRef.current = setTimeout(() => {
        const next = getNextFloor();
        if (next !== null) {
          setTargetFloor(next);
          setPhase("moving");
          setStatusMessage(`moving -> ${next}`);
        } else {
          setDirection(null);
          setTargetFloor(null);
          setPhase("idle");
          setStatusMessage("idle");
        }
      }, 1000);
    }
    return () => {
      if (stopTimeoutRef.current) clearTimeout(stopTimeoutRef.current);
    };
  }, [phase]);

  return (
    <div style={{ minHeight: '100vh', background: 'none', boxShadow: 'none', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
      <div className="login-card hide-scrollbar" style={{ maxWidth: 1500, minWidth: 900, width: '100%', background: 'linear-gradient(135deg, #e3ecfa 0%, #f8fafc 100%)', borderRadius: 28, boxShadow: '0 16px 48px 0 rgba(74,144,226,0.32), 0 4px 16px rgba(44,62,80,0.16)', border: '3px solid #4a90e2', outline: '4px solid #b0c4de', outlineOffset: 3, padding: 48, overflow: 'visible' }}>
        <Grid container spacing={2} alignItems="flex-start">
          <Grid item xs={12} md={7}>
            <Box sx={{ display: "flex", gap: 2, height: props?.numberOfFloors * 100 }}>
              <Elevator
                floors={props.numberOfFloors}
                currentFloor={currentFloor}
                isMoving={phase === "moving"}
                targetFloor={targetFloor}
                moveInterval={moveInterval}
                onRequestFloor={pushRequest}
              />
              <Paper elevation={3} sx={{ flex: 1, overflow: "visible", height: props?.numberOfFloors * 100 }}>
                <Building
                  floors={props.numberOfFloors}
                  currentFloor={currentFloor}
                  isMoving={phase === "moving"}
                  targetFloor={targetFloor}
                  statusMessage={statusMessage}
                  onRequestFloor={pushRequest}
                  name={props.name}
                />
              </Paper>
            </Box>
          </Grid>

          <Grid item xs={12} md={5}>
            <Typography>
              Current Floor: <strong>{currentFloor}</strong>
            </Typography>
            <Typography>
              Status: <strong>{statusMessage}</strong>
            </Typography>

            <Paper elevation={3} sx={{ p: 2, display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', textAlign: 'center' }}>
              <Typography variant="h6" gutterBottom>
                Request Queue
              </Typography>
              {queue.length === 0 ? (
                <Typography variant="body2" color="text.secondary">
                  No requests at the moment
                </Typography>
              ) : (
                <List>
                  {queue
                    .slice()
                    .sort((a, b) => (direction === "up" ? a - b : b - a))
                    .map((floor, index) => (
                      <ListItem key={`${floor}-${index}`}>
                        <ListItemText
                          primary={`Floor ${floor}`}
                          secondary={
                            index === 0
                              ? "Next request"
                              : `Request #${index + 1}`
                          }
                        />
                      </ListItem>
                    ))}
                </List>
              )}
            </Paper>
          </Grid>
        </Grid>
      </div>
    </div>
  );
}
