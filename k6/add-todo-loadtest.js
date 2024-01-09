


import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  vus: 250, // users amount
  duration: '60s', // duration in seconds
};
export default function () {
  http.get('http://localhost:8080/api/Ping');
  sleep(1);
}