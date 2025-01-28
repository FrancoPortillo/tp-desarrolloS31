import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import './index.css'
import App from './App.jsx'
import { Auth0Provider } from '@auth0/auth0-react'


const domain = import.meta.env.VITE_AUTH0_DOMAIN
const clientId = import.meta.env.VITE_AUTH0_CLIENT_ID

createRoot(document.getElementById('root')).render(
    <StrictMode>
      <Auth0Provider
        domain={domain}
        clientId={clientId}
        authorizationParams={{
          redirect_uri: window.location.origin,
          audience: "https://worksense-api/",
          scope: "read:current_user update:current_user_metadata read:employee approve:request"
        }}>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </Auth0Provider>
    </StrictMode>
)
