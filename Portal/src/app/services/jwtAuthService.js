import axios from "axios";
import localStorageService from "./localStorageService";

class JwtAuthService {

  // You need to send http request with email and passsword to your server in this method
  // Your server will return user object & a Token
  // User should have role property
  // You can define roles in app/auth/authRoles.js
  loginWithEmailAndPassword = (email, password) => {
    return new Promise((resolve, reject) => {
      axios.post('https://localhost:44398/oauth/PortalAuthenticate', {email, password})
            .then(response => {
              console.log(response.data);  
              resolve(response.data);                
            })
            .catch(error => {
                reject(error);
            });
    }).then(data => {
      // Login successful
      // Save token
      this.setSession(data.access_token);
      // Set user
      this.setUser(data.user);
      return data;
    });
  };

  // You need to send http requst with existing token to your server to check token is valid
  // This method is being used when user logged in & app is reloaded
  loginWithToken = () => {

    return new Promise((resolve, reject) => {
      let token = localStorage.getItem("jwt_token");
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
      axios.get('https://localhost:44398/oauth/validate')
            .then(response => {
              console.log(response.data);  
              resolve(response.data);                
            })
            .catch(error => {
                reject(error);
            });
    }).then(data => {
      // Token is valid
      this.setSession(data.access_token);
      // Set user
      this.setUser(data.user);
      return data.user;
    });
  };

  logout = () => {
    this.setSession(null);
    this.removeUser();
  }

  // Set token to all http request header, so you don't need to attach everytime
  setSession = token => {
    if (token) {
      localStorage.setItem("jwt_token", token);
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    } else {
      localStorage.removeItem("jwt_token");
      delete axios.defaults.headers.common["Authorization"];
    }
  };

  // Save user to localstorage
  setUser = (user) => {    
    localStorageService.setItem("auth_user", user);
  }
  // Remove user from localstorage
  removeUser = () => {
    localStorage.removeItem("auth_user");
  }
}

export default new JwtAuthService();
