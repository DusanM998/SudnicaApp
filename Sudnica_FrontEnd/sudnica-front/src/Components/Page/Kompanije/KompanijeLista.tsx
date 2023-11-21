import React from 'react'
import { MainLoader } from '../Common';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useDeleteKompanijaMutation, useGetKompanijeQuery } from '../../../apis/kompanijeApi';
import { kompanijaModel } from '../../../Interfaces';

function KompanijeLista() {
    const { data, isLoading } = useGetKompanijeQuery(null);
    const [deleteKontakt] = useDeleteKompanijaMutation();
    const navigate = useNavigate();
  
    const handleKompanijaDelete = async (id: number) => {
      toast.promise(
          deleteKontakt(id),
          {
            pending: 'Vaš zahtev se obrađuje...',
            success: 'Kompanija obrisana uspešno',
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
              <h1 style={{ color: "#2c5785" }}>Lista Kompanija</h1>
                <button 
                  className="btn" 
                  style={{backgroundColor:"#2c5785", color:"white"}}
                  onClick={()=> navigate("/kompanije/azurirajKreirajKompaniju/")}
                  >
                      Dodaj novu kompaniju
                </button>
            </div>
            
            <div className="p-2">
              <div className="row border">
                <div className="col-1">ID</div>
                <div className="col-4">Naziv</div>
                <div className="col-4">Adresa</div>
                <div className="col-3">Akcije</div>
              </div>
              {data && data.result && data.result.map((kompanija: kompanijaModel) => {
                return (
                  <div className="row border" key={kompanija.id}>
                    <div className="col-1">{kompanija.id}</div>
                    <div className="col-4">{kompanija.naziv}</div>
                    <div className="col-4">{kompanija.adresa}</div>
                    <div className="col-3">
                      <button className="btn"
                        style={{ backgroundColor: "#2c5785", color: "white" }} >
                          <i className="bi bi-pencil-fill"
                            onClick={()=> navigate("/kompanije/azurirajKreirajKompaniju/" + kompanija.id)}></i>
                      </button>
                      <button className="btn btn-danger mx-2">
                        <i className="bi bi-trash-fill"
                          onClick={()=> handleKompanijaDelete(kompanija.id ?? 0)}></i>
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

export default KompanijeLista
