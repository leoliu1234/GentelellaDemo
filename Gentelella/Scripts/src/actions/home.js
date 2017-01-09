import { createActions } from 'redux-actions';

// export const increment = createAction('INCREMENT', amount => amount);

export const { increment, decrement } = createActions({
    INCREMENT: amount => amount,
    DECREMENT: amount => amount
});


// export const getThenShow = (dispatch, getState) => {
//     const url = 'http://xxx.json';
//     var b=true;
//     return 1;
//     // setTimeout(function () {
//     //     dispatch(increment(3))
//     // }, 1000);
//     //   fetch(url)
//     //   .then(response => {
//     //     dispatch({
//     //       type: 'SHOW_MESSAGE_FOR_ME',
//     //       message: response.json(),
//     //     });
//     //   }, e => {
//     //     dispatch({
//     //       type: 'FETCH_DATA_FAIL',
//     //       message: e,
//     //     });
//     //   });
// };


export function getThenShow(subreddit) {
    return function (dispatch) {
        dispatch(increment(3))

        // return fetch('file:///D:/Study/Code/redux-actions-demo/test.json')
        //     .then(response => response.json())
        //     .then(number =>
        //         dispatch(increment(number))
        //     )

        setTimeout(()=>{
            dispatch(increment(1));
        },3000);
    }
}