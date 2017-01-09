import React from 'react';

export default class Navbar extends React.Component {
    render() {
        return (
            <div id="navbar-main">
                <div className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div className="navbar-header">
                            <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                <span className="icon icon-shield nav-bar-shield"></span>
                            </button>
                            <a className="navbar-brand hidden-xs hidden-sm" href="#home"><span className="icon icon-shield"></span></a>
                        </div>
                        <div className="navbar-collapse collapse">
                            <ul className="nav navbar-nav">
                                <li><a href="#home" className="smoothScroll">Home</a></li>
                                <li> <a href="#about" className="smoothScroll"> About</a></li>
                                <li> <a href="#services" className="smoothScroll"> Services</a></li>
                                <li> <a href="#team" className="smoothScroll"> Team</a></li>
                                <li> <a href="#portfolio" className="smoothScroll"> Portfolio</a></li>
                                <li> <a href="#blog" className="smoothScroll"> Blog</a></li>
                                <li> <a href="#contact" className="smoothScroll"> Contact</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}