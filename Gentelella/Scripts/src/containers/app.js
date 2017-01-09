import React, { PropTypes } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as DashboardActions from '../actions/dashboard';

class App extends React.Component {
    render() {
        const {increment, counter,decrement,getThenShow} = this.props;
        return (
            <div>{counter}
                <a href="#" onClick={increment.bind(this, 2)}>Increment</a><br />
                <a href="#" onClick={decrement.bind(this, 1)}>Decrement</a><br/>
                <div>leo</div>
                <a href="#" onClick={getThenShow.bind(this, 1)}>test</a><br/>
            </div>
        );
    }
}

App.propTypes = {
    increment: PropTypes.func.isRequired,
    decrement: PropTypes.func.isRequired,
    counter: PropTypes.number.isRequired,
    getThenShow:PropTypes.func.isRequired
};

function mapStateToProps(state) {
    return {
        counter: state.dashboard.counter
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators(DashboardActions, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(App);