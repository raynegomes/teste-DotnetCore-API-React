import styled, { keyframes, css } from 'styled-components';

export const Container = styled.div`
  display: grid;
  background: #fff;
  padding: 30px 20px;
  border-radius: 4px;

  .react-bs-table-container {
    display: grid;
    flex: 1;
  }

  .react-bs-table-no-data {
    text-align: center;
  }
`;

export const Form = styled.form`
  margin-top: 30px;
  display: flex;
  flex-direction: row;
  input {
    flex: 1;
    border: 1px solid ${props => (props.error ? '#ff6b6b' : '#eee')};
    padding: 10px 15px;
    border-radius: 4px;
    font-size: 16px;
    background: ${props => (props.error ? 'rgba(208, 89, 89, 0.08)' : '#fff')};
    margin-right: 15px;
  }
`;

export const LabelError = styled.p`
  display: none;
  flex: 1;
  flex-direction: row;
  color: #ff6b6b;
  font-size: 13px;
  padding: 0;
  padding-top: 10px;
`;

const rotate = keyframes`
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
`;

export const SubmitButton = styled.button.attrs(props => ({
  type: 'submit',
  disabled: props.loading,
}))`
  background: #0084b8;
  color: #fff;
  font-weight: bold;
  border: 0;
  padding: 0 15px;
  margin-left: 10px;
  border-radius: 4px;
  display: flex;
  justify-content: center;
  align-items: center;
  svg {
    margin-right: 0;
  }
  &[disabled] {
    cursor: not-allowed;
    opacity: 0.6;
  }
  ${props =>
    props.loading &&
    css`
      svg {
        animation: ${rotate} 2s linear infinite;
      }
    `}
`;

export const InsertButton = styled.button`
  border-top-left-radius: 0;
  border-bottom-left-radius: 0;
`;
export const EditButton = styled.ul`
  margin-bottom: 0;
`;
