const BASE = (import.meta.env.VITE_API_URL ?? 'http://localhost:5000/api').replace(/\/$/, '')

let authUserId: string | null = localStorage.getItem('userId') || null

export function setUserId(id: string | null) {
  authUserId = id
  if (id) localStorage.setItem('userId', id)
  else localStorage.removeItem('userId')
}

export async function apiFetch(path: string, init: RequestInit = {}) {
  const headers: Record<string,string> = {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    ...(init.headers as Record<string,string> || {})
  }

  if (authUserId) {
    headers['X-User-Id'] = authUserId
  }

  const res = await fetch(`${BASE}${path}`, {
    ...init,
    headers,
    credentials: 'include'
  })

  if (!res.ok) {
    const text = await res.text()
    throw new Error(`${res.status} ${res.statusText} - ${text}`)
  }
  
  if (res.status === 204) return null
  return res.json()
}
