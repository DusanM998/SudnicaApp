import React from 'react'
let crud = require("../Assets/Images/CRUD.png");
let parnica = require("../Assets/Images/parnica.png")
let auth = require("../Assets/Images/auth.jpg")

function Description() {
  return (
    <div className='container row'>
        <div className='my-2'>
            <div className='d-flex justify-content-center pb-5'>
                <h1 style={{color:"#2c5785"}}>O aplikaciji</h1>
            </div>
            <div className='d-flex justify-content-center'>
                <span style={{color:"grey", fontSize:"25px"}}>
                    Cilj aplikacije je implementiranje osnovnih CRUD operacija, sistema autorizacije
                    i autehtifikacije. Aplikacija na osnovu toga podržava postojanje uloga u sistemu 
                    , na osnovu kojih se izdvajaju dva glavna dela aplikacije, administratorski i korisnički.
                </span>
            </div>
        </div>
        <div className='col-md-4 col-12 p-4'>
        <div 
            className='card'
            style={{ boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)" }}
            >
            <div className='card-body pt-2'>
                <div className='row col-10 offset-1 p-2'>
                    <img
                        src={crud}
                        style={{borderRadius:"50%"}}
                        alt=''
                        className='w-100 mt-5 image-box'/>
                </div>
            </div>
            <div className='text-center'>
                <p className='badge bg-secondary' style={{fontSize:"20px"}}>
                    CRUD operacije
                </p>
            </div>
            <p className='card-text' style={{textAlign:"center"}}>
                OPIS
            </p>
        </div>
        </div>
        <div className='col-md-4 col-12 p-4'>
        <div 
            className='card'
            style={{ boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)" }}
            >
            <div className='card-body pt-2'>
                <div className='row col-10 offset-1 p-2'>
                    <img
                        src={parnica}
                        style={{borderRadius:"50%"}}
                        alt=''
                        className='w-100 mt-5 image-box'/>
                </div>
            </div>
            <div className='text-center'>
                <p className='badge bg-secondary' style={{fontSize:"20px"}}>
                    Parnica
                </p>
            </div>
            <p className='card-text' style={{textAlign:"center"}}>
                OPIS
            </p>
        </div>
        </div>
        <div className='col-md-4 col-12 p-4'>
        <div 
            className='card'
            style={{ boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)" }}
            >
            <div className='card-body pt-2'>
                <div className='row col-10 offset-1 p-2'>
                    <img
                        src={auth}
                        style={{borderRadius:"50%"}}
                        alt=''
                        className='w-100 mt-5 image-box'/>
                </div>
            </div>
            <div className='text-center'>
                <p className='badge bg-secondary' style={{fontSize:"20px"}}>
                    Autorizacija & Autentifikacija
                </p>
            </div>
            <p className='card-text' style={{textAlign:"center"}}>
                OPIS
            </p>
        </div>
        </div>     
    </div>
    
  )
}

export default Description
