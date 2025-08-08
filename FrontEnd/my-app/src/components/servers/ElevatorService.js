const API_URL = 'https://localhost:7229/api/Elevator/';

export async function getElevators() {
  const res = await fetch(`${API_URL}GetAllElevator`);
  if (!res.ok) throw new Error('Failed to fetch elevators');
  return res.json();
}

export async function getElevator(id) {
  const res = await fetch(`${API_URL}GetElevatorById/Id?Id=${id}`);
  if (!res.ok) throw new Error('Failed to fetch elevator');
  return res.json();
}
export async function getElevatorByBuilding(id) {
  const res = await fetch(`${API_URL}GetElevatorByBuilding/${id}`);
  if (!res.ok) throw new Error('Failed to fetch elevator');
  return res.json();
}

export async function createElevator(data) {
  const res = await fetch(`${API_URL}AddElevator`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to create elevator');
  return res.json();
}

export async function updateElevator(id, data) {
  const res = await fetch(`${API_URL}UpDateElevator/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to update elevator');
  return res.json();
}

export async function deleteElevator(id) {
  const res = await fetch(`${API_URL}DeleteElevator/Id?Id=${id}`, {
    method: 'DELETE',
  });
  if (!res.ok) throw new Error('Failed to delete elevator');
  return res.json();
}
