import React, { Component } from 'react';
import { FaSpinner } from 'react-icons/fa';
import InputMask from 'react-input-mask';
import { toast } from 'react-toastify';

import { Container, Form, SubmitButton, LabelError } from './styles';

import { formatPrice } from '../../util/format';
import api from '../../services/api';

export default class ProductCreate extends Component {
  state = {
    titlePage: '',
    product: '',
    loading: false,
    error: null,
    createAction: true,
  };

  async componentDidMount() {
    const {
      match: { params },
    } = this.props;

    const productId = params.id;

    // Se for edicao
    if (productId) {
      const response = await api.get(`products/${productId}`);

      if (response.data) {
        const { unityPrice } = response.data;
        const data = {
          ...response.data,
          formattedPrice: unityPrice
            .toFixed(2)
            .toString()
            .replace('.', ','),
        };

        this.setState({
          product: data,
          titlePage: 'Alterar produto',
          createAction: false,
        });
      }
    } else {
      this.setState({
        titlePage: 'Criar novo produto',
      });
    }
  }

  handleNameChange = e => {
    const { product } = this.state;
    const { value } = e.target;

    const updateProduct = {
      ...product,
    };
    updateProduct.name = value;
    this.setState({ product: updateProduct });
  };

  handleAmountChange = e => {
    const { product, error } = this.state;
    const { value } = e.target;

    const updateProduct = {
      ...product,
    };
    updateProduct.amount = parseInt(value);
    this.setState({ product: updateProduct });
  };

  handlePriceChange = e => {
    const { product, error } = this.state;
    const { value } = e.target;

    const updateProduct = {
      ...product,
    };

    updateProduct.unityPrice = parseFloat(value.replace(',', '.'));
    updateProduct.formattedPrice = updateProduct.unityPrice
      .toString()
      .replace('.', ',');

    this.setState({ product: updateProduct });
  };

  handlerSubmit = async e => {
    e.preventDefault();

    this.setState({ loading: true, error: false });
    const labelError = document.querySelector('.error-msg');
    labelError.style.display = 'none';
    labelError.innerHtml = '';

    try {
      const { product, createAction } = this.state;

      if (product === '') {
        throw new Error('Informe os dados do produto');
      }

      if (createAction) {
        await api
          .post('products', {
            name: product.name,
            amount: product.amount,
            unityPrice: product.unityPrice,
          })
          .catch(error => {
            throw new Error(error.response.data.error);
          });
      } else {
        await api
          .put('products', {
            id: product.id,
            name: product.name,
            amount: product.amount,
            unityPrice: product.unityPrice,
          })
          .catch(error => {
            throw new Error(error.response.data.error);
          });
      }

      toast.success('Produto salvo com sucesso.');
      setTimeout(() => {
        window.location.href = `/products`;
      }, 3000);
    } catch (err) {
      if (err.message === 'Request failed with status code 404') {
        toast.error('Repositório não encontrado');
      } else if (err.message === 'Request failed with status code 400') {
        toast.error('Erro ao salvar produto');
      } else {
        labelError.innerText = err.message;
        toast.error(err.message);
      }

      this.setState({ error: true });
    } finally {
      setTimeout(() => {
        this.setState({ loading: false });
      }, 3000);
    }
  };

  render() {
    const { product, error, loading, titlePage } = this.state;

    return (
      <Container>
        <h2>{titlePage}</h2>
        <Form onSubmit={this.handlerSubmit} error={error}>
          <input
            type="text"
            placeholder="Nome do produto"
            defaultValue={product.name || ''}
            onChange={this.handleNameChange}
          />
          <InputMask
            type="number"
            placeholder="Quantidade em estoque"
            defaultValue={product.amount || ''}
            onChange={this.handleAmountChange}
          />
          <InputMask
            type="text"
            placeholder="Valor do produto"
            defaultValue={product.formattedPrice || ''}
            onChange={this.handlePriceChange}
          />

          <SubmitButton loading={loading ? 1 : 0}>
            {loading ? (
              <FaSpinner color="#fff" size={14} />
            ) : (
              <span>Salvar</span>
            )}
          </SubmitButton>
        </Form>
        <LabelError className="error-msg">Error</LabelError>
      </Container>
    );
  }
}
