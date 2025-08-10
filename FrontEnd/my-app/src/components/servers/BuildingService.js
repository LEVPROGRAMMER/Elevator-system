const API_URL = 'https://localhost:7229/api/Building/';

export async function getBuildings() {
  const res = await fetch(`${API_URL}GetAllBuilding`);
  if (!res.ok) throw new Error('Failed to fetch buildings');
  return res.json();
}

export async function getBuilding(id) {  
  const res = await fetch(`${API_URL}GetBuildingByUser/Id?Id=${id}`);
  if (!res.ok) throw new Error('Failed to fetch building');
  return res.json();
}


export async function createBuilding(data) {  
  const res = await fetch(`${API_URL}AddBuilding/Building`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to create building');
  return res.json();
}

export async function updateBuilding(id, data) {
  const res = await fetch(`${API_URL}UpDateBuilding/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to update building');
  return res.json();
}

export async function deleteBuilding(id) {
  const res = await fetch(`${API_URL}DeleteBuilding/Id?Id=${id}`, {
    method: 'DELETE',
  });
  if (!res.ok) throw new Error('Failed to delete building');
  return res.json();
}
