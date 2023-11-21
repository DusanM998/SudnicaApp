import React, { useEffect, useState } from 'react'
import { kontaktModel } from '../../../Interfaces';
import { useDeleteContactMutation, useGetKontaktiQuery } from '../../../apis/kontaktiApi';
import { useNavigate } from 'react-router-dom';
import { MainLoader } from '../Common';
import { toast } from 'react-toastify';

function KontaktiLista() {

  const { data, isLoading } = useGetKontaktiQuery(null);
  const [deleteKontakt] = useDeleteContactMutation();
  const navigate = useNavigate();

  const handleKontaktDelete = async (id: number) => {
    toast.promise(
        deleteKontakt(id),
        {
          pending: 'Vaš zahtev se obrađuje...',
          success: 'Kontakt obrisan uspešno',
          error: 'Došlo je do greške!'
        }
    )
};

  return (
    <>
      {isLoading && <MainLoader />}
      {!isLoading && (
        <div className="table p-5">
          <div className='d-flex align-items-center justify-content-between'>
            <h1 style={{ color: "#2c5785" }}>Lista Kontakata</h1>
              <button 
                className="btn" 
                style={{backgroundColor:"#2c5785", color:"white"}}
                onClick={()=> navigate("/kontakti/azurirajKreirajKontakt")}
                >
                    Dodaj novi kontakt
              </button>
          </div>
          
          <div className="p-2">
            <div className="row border">
              <div className="col-1">ID</div>
              <div className="col-1">Ime</div>
              <div className="col-1">Telefon 1</div>
              <div className="col-1">Telefon 2</div>
              <div className="col-1">Adresa</div>
              <div className="col-2">Email</div>
              <div className="col-1">P/F Lice</div>
              <div className="col-1">Zanimanje</div>
              <div className="col-1">Kompanija</div>
              <div className="col-1">Akcije</div>
            </div>
            {data && data.result && data.result.map((kontakt: kontaktModel) => {
              return (
                <div className="row border" key={kontakt.id}>
                  <div className="col-1">{kontakt.id}</div>
                  <div className="col-1">{kontakt.ime}</div>
                  <div className="col-1">{kontakt.telefon1}</div>
                  <div className="col-1">{kontakt.telefon2}</div>
                  <div className="col-1">{kontakt.adresa}</div>
                  <div className="col-2">{kontakt.email}</div>
                  <div className="col-1">{kontakt.pravnoFizickoLice}</div>
                  <div className="col-1">{kontakt.zanimanje}</div>
                  <div className="col-1">{kontakt.kompanijaId}</div>
                  <div className="col-1">
                    <button className="btn"
                      style={{ backgroundColor: "#2c5785", color: "white" }} >
                        <i className="bi bi-pencil-fill"
                          onClick={()=> navigate("/kontakti/azurirajKreirajKontakt/" + kontakt.id)}></i>
                    </button>
                    <button className="btn btn-danger mx-2">
                      <i className="bi bi-trash-fill"
                        onClick={()=> handleKontaktDelete(kontakt.id ?? 0)}></i>
                    </button>
                  </div>
                </div>
              )
            })}
            
          </div>
        </div>
      )}
      
    </>
    
  )
}

export default KontaktiLista
