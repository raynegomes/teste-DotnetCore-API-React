import React, { Component } from 'react';
import { MdAdd, MdDelete, MdEdit } from 'react-icons/md';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';

import 'react-bootstrap-table/dist/react-bootstrap-table';
import { Container, EditButton, InsertButton } from './styles';

import Footer from '../../components/Footer';

import { formatPrice } from '../../util/format';
import api from '../../services/api';

class Product extends Component {
  state = {
    Products: [],
  };

  getItems = async () => {
    const response = await api.get('products');

    const data = response.data.map(product => ({
      ...product,
      priceFormatted: formatPrice(product.unityPrice),
    }));

    if (response.data) {
      this.setState({ Products: data });
    }
  };

  async componentDidMount() {
    this.getItems();
  }

  handleConfirmDelete = async (next, dropRowKeys) => {
    if (
      window.confirm(
        `${dropRowKeys.length} item(s) serão apagadas.  Deseja realmente apagar as linhas selecionadas?`
      )
    ) {
      const { Products } = this.state;
      let newList = [];

      Promise.all(
        dropRowKeys.map(k => {
          api.delete(`products/${k}`);
          newList = Products.filter(p => {
            return p.id !== k;
          });
          return null;
        })
      ).then(() => {
        this.setState({ Products: newList });
      });
    }
  };

  handleInsertButtonClick = onClick => {
    // return <Redirect to="/products/create" />;
    this.props.history.push(`/products/create`);
  };

  handleDeletetButtonClick = onClick => {
    onClick();
  };

  createCustomInsertButton = onClick => {
    return (
      <InsertButton
        btnText=""
        btnContextual="btn-primary"
        className="btn btn-primary react-bs-table-insert-btn "
        btnGlyphicon="glyphicon-add"
        title="Adicionar itens"
        onClick={() => this.handleInsertButtonClick(onClick)}
      >
        <MdAdd size={20} color="#fff" />
      </InsertButton>
    );
  };

  createCustomDeleteButton = onClick => {
    return (
      <InsertButton
        btnText=""
        btnContextual="btn-danger"
        className="btn btn-danger react-bs-table-del-btn "
        title="Apagar itens"
        onClick={() => this.handleDeletetButtonClick(onClick)}
      >
        <MdDelete size={20} color="#fff" />
      </InsertButton>
    );
  };

  cellButton(cell, row, enumObject, rowIndex) {
    return (
      <EditButton
        btnText=""
        btnContextual="btn-warning"
        className="btn btn-warning "
        btnGlyphicon="glyphicon-add"
        title="Editar itens"
        onClick={() => {
          window.location.href = `/products/update/${row.id}`;
        }}
      >
        <MdEdit size={20} color="#fff" />
      </EditButton>
    );
  }

  render() {
    const { Products } = this.state;

    const selectRowProp = {
      mode: 'checkbox',
    };

    const options = {
      noDataText: 'Não há dados para exibir',
      page: 1, // which page you want to show as default
      sizePerPageList: [
        {
          text: '5',
          value: 5,
        },
        {
          text: '10',
          value: 10,
        },
        {
          text: 'All',
          value: Products.length,
        },
      ], // you can change the dropdown list for size per page
      sizePerPage: 10, // which size per page you want to locate as default
      pageStartIndex: 0, // where to start counting the pages
      paginationSize: 3, // the pagination bar size.
      prePage: 'Anterior', // Previous page button text
      nextPage: 'Próximo', // Next page button text
      firstPage: 'Primeira pág', // First page button text
      lastPage: 'Última página', // Last page button text
      paginationShowsTotal: false, // Accept bool or function
      sortIndicator: true,
      paginationPosition: 'bottom', // default is bottom, top and both is all available
      alwaysShowAllBtns: false, // Always show next and previous button
      deleteBtn: this.createCustomDeleteButton,
      handleConfirmDeleteRow: this.handleConfirmDelete,
      insertBtn: this.createCustomInsertButton,
      // hideSizePerPage: true > You can hide the dropdown for sizePerPage
      // withFirstAndLast: false > Hide the going to First and Last page button
    };

    return (
      <>
        <Container>
          <BootstrapTable
            data={Products}
            bordered={false}
            striped
            hover
            condensed
            pagination
            insertRow
            options={options}
            deleteRow
            selectRow={selectRowProp}
            search
            searchPlaceholder="Pesquisar"
            v={4}
          >
            <TableHeaderColumn isKey dataField="id" hidden />
            <TableHeaderColumn
              dataField="name"
              headerAlign="left"
              dataAlign="left"
              dataSort
            >
              Produto
            </TableHeaderColumn>
            <TableHeaderColumn
              dataField="amount"
              headerAlign="center"
              dataAlign="center"
              dataSort
            >
              Qtd
            </TableHeaderColumn>
            <TableHeaderColumn
              width="50"
              dataField="priceFormatted"
              headerAlign="left"
              dataAlign="center"
              dataSort
            >
              Preço
            </TableHeaderColumn>
            <TableHeaderColumn
              dataFormat={this.cellButton}
              dataAlign="right"
              width="50"
            >
              {' '}
            </TableHeaderColumn>
          </BootstrapTable>
        </Container>
        <Footer />
      </>
    );
  }
}

export default Product;
