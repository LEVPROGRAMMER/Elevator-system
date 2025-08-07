import { createStore } from 'redux';
import userReducer from './Reducer'; // מייבא את ה-reducer שלך

const store = createStore(userReducer);

export default store;
