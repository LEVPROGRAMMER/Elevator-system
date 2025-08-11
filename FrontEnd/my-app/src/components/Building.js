import { Box, Button, Tooltip, Typography } from "@mui/material";
import DirectionsCarFilledIcon from "@mui/icons-material/DirectionsCarFilled";

export default function Building({
  floors,
  currentFloor,
  isMoving,
  targetFloor,
  name,
  statusMessage,
  onRequestFloor
}) {
  return (
    <Box
      className="hide-scrollbar building-visual"
      sx={{
        flex: 1,
        height: floors * 100,
        minHeight: 320,
        minWidth: 320,
        width: '100%',
        overflow: "visible",
        margin: '0 auto',
      }}
    >
      {name && (
        <Typography variant="subtitle1" sx={{ px: 1, py: 0.5 }}>
          Name: {name}
        </Typography>
      )}

      {Array.from({ length: floors }, (_, i) => floors - i).map((floor) => (
        <Box
          key={floor}
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            px: 1,
            height: { xs: 56, sm: 64 },
            borderBottom: (theme) => `1px solid ${theme.palette.divider}`,
          }}
        >
          <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
            <Typography sx={{ width: 78 }} variant="subtitle2">
              floor {floor}
            </Typography>
            <Button size="small" onClick={() => onRequestFloor(floor)}>▲</Button>
            <Button size="small" onClick={() => onRequestFloor(floor)}>▼</Button>
          </Box>

          <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
            {floor === currentFloor && (
              <Tooltip
                title={
                  statusMessage ||
                  (isMoving
                    ? `Elevator is moving to floor ${targetFloor ?? "-"}`
                    : "Elevator is here")
                }
              >
                <Box sx={{ display: "flex", alignItems: "center", gap: 0.5 }}>
                  <Typography variant="caption" color="text.primary">
                    {isMoving ? "Moving" : "Idle"}
                  </Typography>
                </Box>
              </Tooltip>
            )}

          </Box>
        </Box>
      ))}
    </Box>
  );
}
