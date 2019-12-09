import React, { Component } from 'react';
import { Link } from 'react-router-dom';

import logo from '../../assets/images/Logo.png';

import { Container } from './styles';

export default class Header extends Component {
  render() {
    return (
      <Container>
        <Link to="/" replace>
          <img src={logo} alt="Logo" />
        </Link>
      </Container>
    );
  }
}
