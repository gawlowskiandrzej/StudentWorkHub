const axios = require('axios');

describe('Headless Browser Scraper API - Resilience & Resource Tests', () => {
    const BASE_URL = 'http://localhost:3000';

    test('Health check should be responsive', async () => {
        const response = await axios.get(`${BASE_URL}/health`);
        expect(response.status).toBe(200);
        expect(response.data).toBe('OK');
    });

    test('Concurrent requests should not cause the single browser instance to crash', async () => {
        const urls = [
            'https://example.com',
            'https://example.org',
            'https://example.net'
        ];

        const requests = urls.map(url => axios.get(`${BASE_URL}/scrape?url=${encodeURIComponent(url)}`));
        
        const responses = await Promise.all(requests);
        responses.forEach(res => {
            expect(res.status).toBe(200);
            expect(res.data).toContain('<html');
        });
    });

    test('Request with extremely long timeout should be handled or rejected', async () => {
        const slowUrl = 'https://httpbin.org/delay/5';
        const response = await axios.get(`${BASE_URL}/scrape?url=${encodeURIComponent(slowUrl)}`, { timeout: 10000 });
        expect(response.status).toBe(200);
    });

    test('Invalid URLs should return 400/500 but not crash the Node.js process', async () => {
        try {
            await axios.get(`${BASE_URL}/scrape?url=not-a-url`);
        } catch (error) {
            expect([400, 500]).toContain(error.response.status);
        }
        
        const health = await axios.get(`${BASE_URL}/health`);
        expect(health.status).toBe(200);
    });
});
