import React from 'react';
import { Switch, Route } from 'react-router-dom';

import Home from './pages/Home';
import Products from './pages/Products';
import ProductsCreate from './pages/Products/create';

export default function routes() {
  return (
    <Switch>
      <Route path="/" exact component={Home} />
      <Route path="/products" exact component={Products} />
      <Route path="/products/create" exact component={ProductsCreate} />
      <Route path="/products/update/:id" exact component={ProductsCreate} />
      <Route render={() => <h1>Page not found</h1>} />
    </Switch>
  );
}
