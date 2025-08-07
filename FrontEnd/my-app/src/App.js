import { Provider } from 'react-redux';
import store from '../src/redux/Store'; 
import ElevatorServer from './servers/ElevatorServer';
import Login from './components/Login';

function App() {
    return (
        <Provider store={store}> 
            <div className="App">
                <header>
                    <Login></Login>
                    {/* <ElevatorServer></ElevatorServer> */}
                </header>
            </div>
        </Provider>
    );
}

export default App;
