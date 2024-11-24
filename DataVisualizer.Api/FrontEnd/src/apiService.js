import axios from 'axios';

// alle backend api calls
const API_URL = 'http://localhost:5000/api/data';

export const getData = async () => {
  const response = await axios.get(`${API_URL}`);
  return response.data;
};

export const searchData = async (firstName) => {
  const response = await axios.get(`${API_URL}/search`, {
    params: { firstName }
  });
  return response.data;
};

export const filterData = async (state) => {
  const response = await axios.get(`${API_URL}/filter`, {
    params: { state }
  });
  return response.data;
};

export const getStatisticsData = async () => {
  const response = await axios.get(`${API_URL}/statistics`);
  return response.data;
};
