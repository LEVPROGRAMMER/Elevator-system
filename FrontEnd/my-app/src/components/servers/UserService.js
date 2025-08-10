const API_URL = 'https://localhost:7229/api/User/';

export async function getUsers() {
  const res = await fetch(`${API_URL}GetAllUsers`);
  if (!res.ok) throw new Error('Failed to fetch users');
  return res.json();
}

export async function getUser(id) {
  const res = await fetch(`${API_URL}GetUserByPassword/password?password=${id}`);
  if(res.status !== 200) return[];
  if (!res.ok) throw new Error('Failed to fetch user');
  return res.json();
}

export async function createUser(data) {  
  const res = await fetch(`${API_URL}AddUser/User`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to create user');
  return res.json();
}

export async function updateUser(id, data) {
  const res = await fetch(`${API_URL}UpDateUser/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to update user');
  return res.json();
}

export async function deleteUser(id) {
  const res = await fetch(`${API_URL}DeleteUser/Id?Id=${id}`, {
    method: 'DELETE',
  });
  if (!res.ok) throw new Error('Failed to delete user');
  return res.json();
}
