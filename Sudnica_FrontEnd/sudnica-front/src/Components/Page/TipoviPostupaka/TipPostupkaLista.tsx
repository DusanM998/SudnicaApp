import React from 'react'
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { MainLoader } from '../Common';
import { tipPostupkaModel } from '../../../Interfaces';
import { useDeleteTipPostupkaMutation, useGetTipoviPostupakaQuery } from '../../../apis/tipPostupkaApi';

function TipPostupkaLista() {
  const { data, isLoading } = useGetTipoviPostupakaQuery(null);
    const [deleteLokacija] = useDeleteTipPostupkaMutation();
    const navigate = useNavigate();
  
    const handleTipPostupkaDelete = async (id: number) => {
      toast.promise(
        deleteLokacija(id),
          {
            pending: 'Vaš zahtev se obrađuje...',
            success: 'Tip Postupka obrisan uspešno',
            error: 'Došlo je do greške!'
          }
      )
    };
    //console.log(data);
  
    return (
      <>
        {isLoading && <MainLoader />}
        {!isLoading && (
          <div className="table p-5">
            <div className='d-flex align-items-center justify-content-between'>
              <h1 style={{ color: "#2c5785" }}>Lista Tipova Postupaka</h1>
                <button 
                  className="btn" 
                  style={{backgroundColor:"#2c5785", color:"white"}}
                  onClick={()=> navigate("/tipPostupka/azurirajKreirajTip")}
                  >
                      Dodaj novi Tip Postupka
                </button>
            </div>
            
            <div className="p-2">
              <div className="row border">
                <div className="col-4">ID</div>
                <div className="col-5">Naslov</div>
                <div className="col-3">Akcije</div>
              </div>
              {data && data && data.map((tipPostupka: tipPostupkaModel) => {
                return (
                  <div className="row border" key={tipPostupka.id}>
                    <div className="col-4">{tipPostupka.id}</div>
                    <div className="col-5">{tipPostupka.naslov}</div>
                    <div className="col-3">
                      <button className="btn"
                        style={{ backgroundColor: "#2c5785", color: "white" }} >
                          <i className="bi bi-pencil-fill"
                            onClick={()=> navigate("/tipPostupka/azurirajKreirajTip/" + tipPostupka.id)}></i>
                      </button>
                      <button className="btn btn-danger mx-2">
                        <i className="bi bi-trash-fill"
                          onClick={()=> handleTipPostupkaDelete(tipPostupka.id ?? 0)}></i>
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

export default TipPostupkaLista
