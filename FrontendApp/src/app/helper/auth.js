const TOKEN_KEY = 'local-user-Token';
const ID_KEY = 'local-user-Id';
const USER_INFO_KEY = 'userInfo';

export const setToken = (token) => {
  window.localStorage.setItem(TOKEN_KEY, token);
};

export const setId = (id) => {
  window.localStorage.setItem(ID_KEY, id);
};

export const getToken = () => {
  return window.localStorage.getItem(TOKEN_KEY);
};

export const getId = () => {
  return window.localStorage.getItem(ID_KEY);
};

export const getUserInfo = () => {
  const id = getId();
  const token = getToken();

  const userInfo = id && token ? { id, token } : null;
  
  // console.log('Token:', userInfo?.token);
  // console.log('ID:', userInfo?.id);
  // console.log('getUserInfo:', userInfo);  // debug line

  return userInfo;
};

export const setUserInfo = (userInfo) => {
  const data = JSON.stringify(userInfo);
  window.localStorage.setItem(USER_INFO_KEY, data);
};

export const setUserSession = (token, userInfo) => {
  setToken(token);
  setUserInfo(userInfo);
  setId(userInfo.id);  // store user id separately
};

export const clearUserSession = () => {
  window.localStorage.removeItem(TOKEN_KEY);
  window.localStorage.removeItem(ID_KEY);
  window.localStorage.removeItem(USER_INFO_KEY);
};

export const userIsOwner = (item) => {
  const userInfo = getUserInfo();
  return userInfo && userInfo.id.toString() === item.createdBy.toString(); 
};

export const userIsAuthenticated = () => {
  const userInfo = getUserInfo();
  return Boolean(userInfo && userInfo.token);  
};