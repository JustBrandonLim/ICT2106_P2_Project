import React from 'react';

const DialogMessage = ({ message, onClose }) => {
  return (
    <div className="dialog">
      <div className="dialog-content">
        <p>{message}</p>
        <button onClick={onClose}>OK</button>
      </div>
    </div>
  );
};

export default DialogMessage