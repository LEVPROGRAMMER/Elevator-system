import React from 'react';
import { Provider } from 'react-redux';
import store from '../src/redux/Store'; // מייבא את ה-Store
import Login from './components/Login';
import ElevatorSimulation from './components/Building';
import Building from './components/Building';
import ElevatorStatus from './servers/ElevatorServer';
import AddBuilding from './components/AddBuilding';
import BuildingView from './components/BuildingView';

function App() {
    return (
        <Provider store={store}> {/* חיבור ה-Store */}
            <div className="App">
                <header>
                    {/* <AddBuilding></AddBuilding> */}
                    <Login></Login>
                    {/* <ElevatorStatus></ElevatorStatus> */}
                    {/* <BuildingView></BuildingView> */}
                    {/* <Building></Building> */}
                </header>
            </div>
        </Provider>
    );
}

export default App;
