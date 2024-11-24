import React, { useState, useEffect } from 'react';
import { getData, searchData, filterData, getStatisticsData } from './apiService';

const DataTable = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [stateFilter, setStateFilter] = useState('');
  const [firstNameSearch, setFirstNameSearch] = useState('');
  const [statistics, setStatistics] = useState(null);
  const [showingAll, setShowingAll] = useState(false); 


  const fetchAllData = async () => {
    setLoading(true);
    setStatistics(null); 
    setShowingAll(true); 
    try {
      const result = await getData();
      setData(result.slice(0, 100)); 
    } catch (error) {
      setError('Failed to load data');
    } finally {
      setLoading(false);
    }
  };

  // hent search data
  const searchDataByFirstName = async (firstName) => {
    setLoading(true);
    setStatistics(null);
    setData([]); // Clear data before loading new data
    setShowingAll(false); 
    try {
      const result = await searchData(firstName);
      setData(result.slice(0, 100)); // Limit til 100 
    } catch (error) {
      setError('Failed to search data');
    } finally {
      setLoading(false);
    }
  };

  // Fetcher filtering med stat 
  const filterDataByState = async (state) => {
    setLoading(true);
    setStatistics(null);
    setData([]); 
    setShowingAll(false); 
    try {
      const result = await filterData(state);
      setData(result.slice(0, 100));
    } catch (error) {
      setError('Failed to filter data');
    } finally {
      setLoading(false);
    }
  };

  // Fetch statistics
  const fetchStatistics = async () => {
    setLoading(true);
    setData([]); 
    setShowingAll(false); 
    try {
      const result = await getStatisticsData();
      setStatistics(result);
    } catch (error) {
      setError('Failed to load statistics');
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div>
      <h2>Data Table</h2>

      <button onClick={fetchAllData}>Show All Data</button>

      <input
        type="text"
        placeholder="Search by First Name"
        value={firstNameSearch}
        onChange={(e) => setFirstNameSearch(e.target.value)}
      />
      <button onClick={() => searchDataByFirstName(firstNameSearch)}>Search</button>

    
      <input
        type="text"
        placeholder="Filter by State"
        value={stateFilter}
        onChange={(e) => setStateFilter(e.target.value)}
      />
      <button onClick={() => filterDataByState(stateFilter)}>Filter</button>

     
      <button onClick={fetchStatistics}>Show Statistics</button>

      {statistics && (
        <div>
          <h3>Statistics</h3>
          <ul>
            {statistics.map((stat) => (
              <li key={stat.state}>
                <strong>{stat.state}</strong> - Avg Age: {stat.averageAge}, Most Common First Name: {stat.mostCommonFirstName}, Most Common Last Name: {stat.mostCommonLastName}
              </li>
            ))}
          </ul>
        </div>
      )}

    
      {data.length > 0 && (
        <table>
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Age</th>
              <th>Street</th>
              <th>City</th>
              <th>State</th>
              <th>Latitude</th>
              <th>Longitude</th>
              <th>Credit Card</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item) => (
              <tr key={item.seq}>
                <td>{item.nameFirst}</td>
                <td>{item.nameLast}</td>
                <td>{item.age}</td>
                <td>{item.street}</td>
                <td>{item.city}</td>
                <td>{item.state}</td>
                <td>{item.latitude}</td>
                <td>{item.longitude}</td>
                <td>{item.ccNumber}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default DataTable;
