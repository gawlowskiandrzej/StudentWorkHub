import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  stages: [
    { duration: '30s', target: 20 },  // Ramp up to 20 users
    { duration: '1m', target: 20 },   // Stay at 20 users
    { duration: '30s', target: 50 },  // Ramp up to 50 users
    { duration: '1m', target: 50 },   // Stay at 50 users
    { duration: '30s', target: 0 },   // Ramp down to 0 users
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'], // 95% of requests should be below 500ms
    http_req_failed: ['rate<0.01'],   // Error rate should be less than 1%
  },
};

const BASE_URL = __ENV.BASE_URL || 'https://studentworkhub.com';

export default function () {
  // Test homepage
  let homeResponse = http.get(`${BASE_URL}/`);
  check(homeResponse, {
    'homepage status is 200': (r) => r.status === 200,
    'homepage loads in <500ms': (r) => r.timings.duration < 500,
  });
  
  sleep(1);
  
  // Test search page
  let searchResponse = http.get(`${BASE_URL}/search`);
  check(searchResponse, {
    'search page status is 200': (r) => r.status === 200,
  });
  
  sleep(1);
  
  // Test API endpoint
  let apiResponse = http.get(`${BASE_URL}/api/health`);
  check(apiResponse, {
    'API health check is 200': (r) => r.status === 200,
  });
  
  sleep(1);
}
