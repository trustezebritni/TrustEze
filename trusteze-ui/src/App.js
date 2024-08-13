import './App.css';
import React, { useState, useEffect } from 'react';



function App() {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch('http://api.trusteze.co/listings')
      .then(response => response.json())
      .then(json => setData(json))
      .catch(error => console.error(error));
  }, []);

  return (
    <div>
      {data ? <pre>{createTable(data)}</pre> : 'Loading...'}
    </div>
  );
}
function createTable(data) {

  return (
    <table>
      <tr key={"header"}>
        <th>Id</th>
        <th>Street Name</th>
      </tr>
      {data.results.map((item) => (
        <tr key="123">
         <td> {item.id}</td>
         <td> {item.standardFields.streetName}</td>
        </tr>
      ))}
    </table>
  );
}

export default App;