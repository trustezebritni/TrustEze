import './App.css';
import React, { useState, useEffect } from 'react';


// function App() {

//   const [data, setData] = useState(null);

//   function handleClick() {
//     const xhr = new XMLHttpRequest();
//     xhr.open('GET', 'http://api.trusteze.co/listings');
//     xhr.onload = function() {
//       if (xhr.status === 200) {
//         setData(JSON.parse(xhr.responseText));
//         console.log('SUCCESS');
//       }
//       else
//       {
//         console.log('FAILURE');
//       }
//     };
//     xhr.send();
//   }


//   return (
//     <div>
//     <button onClick={handleClick}>Get Data</button>
//     {data ? <div>{JSON.stringify(data)}</div> : <div>Loading...</div>}
//   </div>
//   );
// }

// export default App;


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
      {data ? <pre>{JSON.stringify(data, null, 2)}</pre> : 'Loading...'}
    </div>
  );
}

export default App;