import { Box, Button, Tooltip, Typography, Paper } from "@mui/material";
import { keyframes } from "@mui/system";
import { useState } from "react";

const blink = keyframes`
  0%, 100% { background-color: #bfdbfe; }
  50% { background-color: #3b82f6; color: white; }
`;

export default function Building({
  floors,
  currentFloor,
  isMoving,
  targetFloor,
  name,
  statusMessage,
  onRequestFloor,
}) {
  const [requestedFloors, setRequestedFloors] = useState([]);

  const handleRequestFloor = (floor) => {
    if (!requestedFloors.includes(floor)) {
      setRequestedFloors((prev) => [...prev, floor]);
    }
    onRequestFloor(floor);
  };

  if (requestedFloors.includes(currentFloor)) {
    setTimeout(() => {
      setRequestedFloors((prev) => prev.filter((f) => f !== currentFloor));
    }, 300);
  }

  return (
    <Paper
      elevation={6}
      sx={{
        flex: 1,
        minHeight: 360,
        minWidth: 300,
        borderRadius: 3,
        background: "linear-gradient(180deg,#f9fafb,#ffffff)",
      }}
    >
      {name && (
        <Typography
          variant="h6"
          sx={{
            px: 2,
            py: 1,
            fontWeight: 700,
            borderBottom: "2px solid #e2e8f0",
            bgcolor: "#f8fafc",
          }}
        >
          {name}
        </Typography>
      )}

      <Box
        className="hide-scrollbar building-visual"
        sx={{
          height: "100%",
          overflowY: "auto",
          p: 1,
        }}
      >
        {Array.from({ length: floors }, (_, i) => floors - i).map((floor) => {
          const isHere = floor === currentFloor;
          const isTarget = floor === targetFloor;
          const isRequested = requestedFloors.includes(floor);

          return (
            <Box
              key={floor}
              sx={{
                display: "flex",
                alignItems: "center",
                justifyContent: "space-between",
                px: 2,
                py: 1,
                mb: 0.5,
                borderRadius: 2,
                bgcolor: isHere ? "#dbeafe" : "#f9fafb",
                border: isHere ? "2px solid #3b82f6" : "1px solid #e2e8f0",
                transition: "all 0.3s ease",
              }}
            >
              <Box sx={{ display: "flex", alignItems: "center", gap: 1.5 }}>
                <Typography
                  sx={{
                    width: 70,
                    fontWeight: 600,
                    color: isHere ? "#1e3a8a" : "text.secondary",
                  }}
                  variant="subtitle2"
                >
                  Floor {floor}
                </Typography>
                <Button
                  size="small"
                  variant="outlined"
                  onClick={() => handleRequestFloor(floor)}
                  sx={{
                    borderRadius: 2,
                    minWidth: 32,
                    animation: isRequested ? `${blink} 1s infinite` : "none",
                  }}
                >
                  ▲
                </Button>
                <Button
                  size="small"
                  variant="outlined"
                  onClick={() => handleRequestFloor(floor)}
                  sx={{
                    borderRadius: 2,
                    minWidth: 32,
                    animation: isRequested ? `${blink} 1s infinite` : "none",
                  }}
                >
                  ▼
                </Button>
              </Box>

              <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                {isHere && (
                  <Tooltip
                    title={
                      statusMessage ||
                      (isMoving
                        ? `Elevator moving to floor ${targetFloor ?? "-"}`
                        : "Elevator is here")
                    }
                  >
                    <Box
                      sx={{
                        display: "flex",
                        alignItems: "center",
                        px: 1,
                        py: 0.3,
                        borderRadius: 2,
                        bgcolor: isMoving ? "#fef3c7" : "#dcfce7",
                        border: isMoving
                          ? "1px solid #facc15"
                          : "1px solid #22c55e",
                      }}
                    >
                      <Typography
                        variant="caption"
                        sx={{
                          fontWeight: 600,
                          color: isMoving ? "#92400e" : "#166534",
                        }}
                      >
                        {isMoving ? "Moving" : "Idle"}
                      </Typography>
                    </Box>
                  </Tooltip>
                )}

                {isTarget && !isHere && (
                  <Typography
                    variant="caption"
                    sx={{ color: "#3b82f6", fontWeight: 600 }}
                  >
                  </Typography>
                )}
              </Box>
            </Box>
          );
        })}
      </Box>
    </Paper>
  );
}
