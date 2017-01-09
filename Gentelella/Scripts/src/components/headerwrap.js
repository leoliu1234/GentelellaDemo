import React from 'react';

export default class HeaderWrap extends React.Component {
    render() {
        return (
            <div id="home" name="home">
                <header className="clearfix">
                    <h1><span className="icon icon-shield"></span></h1>
                    <p>A Bootstrap 3 One Page Theme.</p>
                    <p>Exclusive for BlackTie.co.</p>
                </header>
            </div>
        );
    }
}