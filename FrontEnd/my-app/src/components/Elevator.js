import { Box, Paper, Typography, Button, Grid } from "@mui/material";

export default function Elevator({
  floors,
  currentFloor,
  isMoving,
  moveInterval,
  onRequestFloor
}) {
  const floorHeightPercent = 100 / floors;
  const cabinTopPercent = (floors - currentFloor) * floorHeightPercent;

  return (
    <Paper
      elevation={6}
      sx={{
        width: 240,
        minHeight: 360,
        bgcolor: "background.paper",
        position: "relative",
        p: 1,
      }}
    >
      <Typography variant="caption">
        elevator - current floor : <strong>{currentFloor}</strong>
      </Typography>

      <Box
        sx={{
          mt: 1,
          position: "relative",
          height: { xs: 320, sm: Math.min(64 * floors, 560) },
          border: (theme) => `2px solid ${theme.palette.divider}`,
          borderRadius: 1,
          overflow: "hidden",
          background: "linear-gradient(180deg, #fafbff, #fffef6)",
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
                borderTop: "1px dashed rgba(0,0,0,0.06)",
              }}
            />
          );
        })}

        <Box
          sx={{
            position: "absolute",
            left: "50%",
            transform: "translateX(-50%)",
            width: 100,
            top: `${cabinTopPercent}%`,
            transition: `top ${Math.max(300, moveInterval)}ms cubic-bezier(.22,.9,.2,1)`,
            boxShadow: (theme) =>
              `0 6px 14px ${theme.palette.mode === "light"
                ? "rgba(18,38,63,0.08)"
                : "rgba(0,0,0,0.6)"
              }`,
          }}
        >
          <Paper
            elevation={3}
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              height: 56,
              borderRadius: 1,
              background: "linear-gradient(180deg,#ffffff,#f4f8ff)",
              border: "1px solid rgba(0,0,0,0.06)",
            }}
          >
            <Typography variant="subtitle2">elevator</Typography>
          </Paper>
        </Box>
      </Box>

      {!isMoving && (
        <Grid container spacing={1} sx={{ mt: 1 }}>
          {Array.from({ length: floors }, (_, idx) => idx + 1).map((f) => (
            <Grid item xs={6} key={f}>
              <Button
                variant="outlined"
                fullWidth
                size="small"
                onClick={() => onRequestFloor(f)}
              >
                floor {f}
              </Button>
            </Grid>
          ))}
        </Grid>
      )}
    </Paper>
  );
};
