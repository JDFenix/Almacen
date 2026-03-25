import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import { env } from 'process';

// URL del backend en desarrollo (fuera de Docker)
// Puedes sobreescribirla con VITE_DEV_API_URL si lo necesitas.
const devApiTarget =
    env.VITE_DEV_API_URL ??
    (env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5190');

export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/api': {
                target: devApiTarget,
                secure: false,
                changeOrigin: true
            },
            // Proxy también para el endpoint de ejemplo weatherforecast del backend
            '^/weatherforecast': {
                target: devApiTarget,
                secure: false,
                changeOrigin: true
            }
        },
        // Puerto estándar de Vite (fijo) para evitar conflictos con launch.json
        port: 5173
    }
})