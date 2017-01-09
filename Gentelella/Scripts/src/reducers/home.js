import { handleActions } from 'redux-actions';

const home = handleActions({
    INCREMENT: (state, action) => ({
        counter: state.counter + action.payload
    }),
    DECREMENT: (state, action) => ({
        counter: state.counter - action.payload
    })
}, { counter: 0 });

export default home;