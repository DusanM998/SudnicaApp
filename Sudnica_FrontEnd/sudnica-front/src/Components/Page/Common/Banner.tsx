import React, { useState } from 'react';
import "./banner.css";
import { useNavigate } from 'react-router-dom';

function Banner() {

  const navigate = useNavigate();

  return (
    <div className="custom-banner">
      <div
        className="m-auto d-flex align-items-center"
        style={{
          width: "400px",
          height: "50vh",
        }}
      >
        <div className="d-flex align-items-center" style={{ width: "100%" }}>
          <button
            className="form-control rounded-pill"
            style={{
              width: "100%",
              padding: "40px 40px",
              backgroundColor: "#f3c32b",
              color: "white",
              border:"2px solid #2c5785",
              boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)"
            }}
            onClick={()=> navigate("/parnice/parniceLista")}
          >Pregled Parnica</button>
          <span style={{ position: "relative", left: "-43px", color:"#2c5785" }}>
            <i className="bi bi-search"></i>
          </span>
        </div>
      </div>
    </div>
  )
}

export default Banner
