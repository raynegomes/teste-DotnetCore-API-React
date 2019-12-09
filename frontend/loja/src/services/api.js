import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5000/api/v1/',
  headers: {
    'Access-Control-Allow-Origin': '*',
    'Content-Type': 'application/json',
    mode: 'no-cors',
  },
});

export default api;
