import Cookies from 'js-cookie'

let exports = {};

function decodeToken() {
    const jwt = Cookies.get('token');
    let jwtData = jwt.split('.')[1]
    let decodedJwtJsonData = window.atob(jwtData)
    let decodedJwtData = JSON.parse(decodedJwtJsonData)
    return(decodedJwtData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"])
};
exports.isAdmin = (that) => {
    const jwt = Cookies.get('token');
    let jwtData = jwt.split('.')[1]
    let decodedJwtJsonData = window.atob(jwtData)
    let decodedJwtData = JSON.parse(decodedJwtJsonData)
    return(decodedJwtData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "Admin")
};
exports.isEmployee = (that) => { 
    const jwt = Cookies.get('token');
    let jwtData = jwt.split('.')[1]
    let decodedJwtJsonData = window.atob(jwtData)
    let decodedJwtData = JSON.parse(decodedJwtJsonData)
    return(decodedJwtData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "Employee")
};
exports.isFAManager = (that) => {
  const jwt = Cookies.get('token');
  let jwtData = jwt.split('.')[1]
  let decodedJwtJsonData = window.atob(jwtData)
  let decodedJwtData = JSON.parse(decodedJwtJsonData)
  return(decodedJwtData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "FAManager")
};
exports.getUsername = (that) => {
  const jwt = Cookies.get('token');
  let jwtData = jwt.split('.')[1]
  let decodedJwtJsonData = window.atob(jwtData)
  let decodedJwtData = JSON.parse(decodedJwtJsonData)
  return(decodedJwtData["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"])
};
exports.getToken = (that) => {
  return(Cookies.get('token'))
};

exports.logout = (that) => {
  Cookies.remove('token');
}
exports.isLoggedIn = (that) => {
  const jwt = Cookies.get('token');
  return (jwt != undefined)
}

export default exports;