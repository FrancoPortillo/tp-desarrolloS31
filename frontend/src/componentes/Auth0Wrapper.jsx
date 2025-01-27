import { Auth0Provider } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";

export const Auth0Wrapper = ({ children }) => {
  const navigate = useNavigate();

  const domain = import.meta.env.VITE_AUTH0_DOMAIN;
  const clientId = import.meta.env.VITE_AUTH0_CLIENT_ID;
  const redirectUri = import.meta.env.VITE_AUTH0_CALLBACK_URL;

  const onRedirectCallback = (appState) => {
    navigate(appState?.returnTo || window.location.pathname);
  };

  if (!(domain && clientId && redirectUri)) {
    return null;
  }
  
  return (
    <Auth0Provider
      domain={domain}
      clientId={clientId}
      authorizationParams={{
        redirect_uri: redirectUri,
      }}
      onRedirectCallback={onRedirectCallback}
    >
      {children}
    </Auth0Provider>
  );
};

export default Auth0Wrapper;
// import { Auth0Provider } from "@auth0/auth0-react";
// import { useNavigate } from "react-router-dom";

// export const Auth0Wrapper = ({ children }) => {
//   const navigate = useNavigate();

//   const domain = import.meta.env.VITE_AUTH0_DOMAIN;
//   const clientId = import.meta.env.VITE_AUTH0_CLIENT_ID;
//   const redirectUri = import.meta.env.VITE_AUTH0_CALLBACK_URL;

//   console.log("Domain:", domain);
//   console.log("Client ID:", clientId);
//   console.log("Redirect URI:", redirectUri);

//   const onRedirectCallback = (appState) => {
//     navigate(appState?.returnTo || window.location.pathname);
//   };

//   if (!(domain && clientId && redirectUri)) {
//     console.log("Missing Auth0 configuration");
//     return null;
//   }

//   return (
//     <Auth0Provider
//       domain={domain}
//       clientId={clientId}
//       authorizationParams={{
//         redirect_uri: redirectUri,
//       }}
//       onRedirectCallback={onRedirectCallback}
//     >
//       {children}
//     </Auth0Provider>
//   );
// };