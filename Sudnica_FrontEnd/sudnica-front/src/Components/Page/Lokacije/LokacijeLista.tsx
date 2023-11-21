import React from 'react'
import { useNavigate } from 'react-router-dom';
import { useDeleteLokacijaMutation, useGetLokacijeQuery } from '../../../apis/lokacijeApi';
import { MainLoader } from '../Common';
import { toast } from 'react-toastify';
import { lokacijaModel } from '../../../Interfaces';

function LokacijeLista() {
    const { data, isLoading } = useGetLokacijeQuery(null);
    const [deleteLokacija] = useDeleteLokacijaMutation();
    const navigate = useNavigate();
  
    const handleLokacijaDelete = async (id: number) => {
      toast.promise(
        deleteLokacija(id),
          {
            pending: 'Vaš zahtev se obrađuje...',
            success: 'Lokacija obrisana uspešno',
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
              <h1 style={{ color: "#2c5785" }}>Lista Lokacija</h1>
                <button 
                  className="btn" 
                  style={{backgroundColor:"#2c5785", color:"white"}}
                  onClick={()=> navigate("/lokacije/azurirajKreirajLokaciju")}
                  >
                      Dodaj novu lokaciju
                </button>
            </div>
            
            <div className="p-2">
              <div className="row border">
                <div className="col-4">ID</div>
                <div className="col-5">Naslov</div>
                <div className="col-3">Akcije</div>
              </div>
              {data && data && data.map((lokacija: lokacijaModel) => {
                return (
                  <div className="row border" key={lokacija.id}>
                    <div className="col-4">{lokacija.id}</div>
                    <div className="col-5">{lokacija.naslov}</div>
                    <div className="col-3">
                      <button className="btn"
                        style={{ backgroundColor: "#2c5785", color: "white" }} >
                          <i className="bi bi-pencil-fill"
                            onClick={()=> navigate("/lokacije/azurirajKreirajLokaciju/" + lokacija.id)}></i>
                      </button>
                      <button className="btn btn-danger mx-2">
                        <i className="bi bi-trash-fill"
                          onClick={()=> handleLokacijaDelete(lokacija.id ?? 0)}></i>
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

export default LokacijeLista
