const API_URL = 'https://localhost:7229/api/ElevatorCalls/';

export async function getElevatorCalls() {
  const res = await fetch(`${API_URL}GetAllElevatorCall`);
  if (!res.ok) throw new Error('Failed to fetch elevator calls');
  return res.json();
}

export async function getElevatorCall(id) {
  const res = await fetch(`${API_URL}GetElevatorCallById/Id?Id=${id}`);
  if (!res.ok) throw new Error('Failed to fetch elevator call');
  return res.json();
}

export async function createElevatorCall(data) {
  const res = await fetch(`${API_URL}AddElevatorCall/ElevatorCall`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to create elevator call');
  return res.json();
}

export async function updateElevatorCall(id, fileName,data) {
  const res = await fetch(`${API_URL}UpDateElevatorCall/${id}fileName=${fileName}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to update elevator call');
  return res.json();
}

export async function deleteElevatorCall(id) {
  const res = await fetch(`${API_URL}DeleteElevatorCall/Id?Id=${id}`, {
    method: 'DELETE',
  });
  if (!res.ok) throw new Error('Failed to delete elevator call');
  return res.json();
}
