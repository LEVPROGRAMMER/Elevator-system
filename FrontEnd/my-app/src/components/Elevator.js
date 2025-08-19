import { Box, Paper, Typography, Button, Grid } from "@mui/material";
import { useState } from "react";

export default function Elevator({
  floors,
  currentFloor,
  isMoving,
  moveInterval,
  onRequestFloor,
}) {
  const [targetFloor, setTargetFloor] = useState(null);

  const floorHeightPercent = 100 / floors;
  const cabinTopPercent = (floors - currentFloor) * floorHeightPercent;

  if (!isMoving && targetFloor === currentFloor) {
    setTimeout(() => setTargetFloor(null), 300);
  }

  return (
    <Paper
      elevation={8}
      sx={{
        width: 260,
        minHeight: 400,
        bgcolor: "background.paper",
        position: "relative",
        p: 2,
        borderRadius: 3,
        background: "linear-gradient(180deg,#f8fafc,#f1f5f9)",
        boxShadow: "0 8px 20px rgba(0,0,0,0.1)",
      }}
    >
      <Typography
        variant="subtitle2"
        sx={{ mb: 1, color: "text.secondary", fontWeight: 600 }}
      >
        Elevator – Current Floor:{" "}
        <Typography component="span" variant="subtitle1" sx={{ fontWeight: 700 }}>
          {currentFloor}
        </Typography>
      </Typography>

      <Box
        sx={{
          mt: 1,
          position: "relative",
          height: { xs: 340, sm: Math.min(70 * floors, 600) },
          border: "3px solid #cbd5e1",
          borderRadius: 2,
          overflow: "hidden",
          background: "linear-gradient(180deg,#f9fafb,#fefefe)",
        }}
      >
        {Array.from({ length: floors }).map((_, idx) => {
          const top = (idx / floors) * 100;
          return (
            <Box
              key={idx}
              sx={{
                position: "absolute",
                left: 0,
                right: 0,
                top: `${top}%`,
                height: 0,
                borderTop: "1px dashed rgba(0,0,0,0.08)",
              }}
            />
          );
        })}

        <Box
          sx={{
            position: "absolute",
            left: "50%",
            transform: "translateX(-50%)",
            width: 120,
            top: `${cabinTopPercent}%`,
            transition: `top ${Math.max(
              300,
              moveInterval
            )}ms cubic-bezier(.22,.9,.2,1)`,
            zIndex: 2,
          }}
        >
          <Paper
            elevation={6}
            sx={{
              position: "relative",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              height: 80,
              borderRadius: 2,
              background: "linear-gradient(180deg,#ffffff,#e2e8f0)",
              border: "2px solid #94a3b8",
              overflow: "hidden",
            }}
          >
            <Box
              sx={{
                position: "absolute",
                left: 0,
                top: 0,
                bottom: 0,
                width: isMoving ? "50%" : "0%",
                background: "linear-gradient(180deg,#cbd5e1,#94a3b8)",
                transition: "width 600ms ease",
                zIndex: 3,
              }}
            />
            <Box
              sx={{
                position: "absolute",
                right: 0,
                top: 0,
                bottom: 0,
                width: isMoving ? "50%" : "0%",
                background: "linear-gradient(180deg,#cbd5e1,#94a3b8)",
                transition: "width 600ms ease",
                zIndex: 3,
              }}
            />

            <Typography variant="body1" sx={{ fontWeight: 600, zIndex: 2 }}>
               Elevator
            </Typography>
          </Paper>
        </Box>
      </Box>

      <Grid container spacing={1.5} sx={{ mt: 2 }}>
        {Array.from({ length: floors }, (_, idx) => idx + 1).map((f) => {
          const isBlinking = targetFloor === f && isMoving;

          return (
            <Grid item xs={6} key={f}>
              <Button
                variant="contained"
                color="primary"
                fullWidth
                size="small"
                onClick={() => {
                  setTargetFloor(f);
                  onRequestFloor(f);
                }}
                sx={{
                  borderRadius: 2,
                  textTransform: "none",
                  fontWeight: 600,
                  background: isBlinking
                    ? "linear-gradient(145deg,#facc15,#eab308)"  
                    : "linear-gradient(145deg,#3b82f6,#2563eb)",
                  animation: isBlinking
                    ? "blinker 1s linear infinite"
                    : "none",
                  "@keyframes blinker": {
                    "50%": { opacity: 0.4 },
                  },
                  "&:hover": {
                    background: isBlinking
                      ? "linear-gradient(145deg,#facc15,#eab308)"
                      : "linear-gradient(145deg,#2563eb,#1d4ed8)",
                  },
                }}
              >
                Floor {f}
              </Button>
            </Grid>
          );
        })}
      </Grid>
    </Paper>
  );
}
