import React from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { useGetParnicaByIdQuery } from '../../../apis/parniceApi';
import { userModel } from '../../../Interfaces';
import { useSelector } from 'react-redux';
import { RootState } from '../../../Storage/Redux/store';
import { MainLoader } from '../Common';
import { SD_Roles } from '../../../Utility/SD';

function ParnicaDetalji() {

    const navigate = useNavigate();
    const { parnicaId } = useParams();
    const { data, isLoading } = useGetParnicaByIdQuery(parnicaId);
    const userData: userModel = useSelector((state: RootState) => state.userAuthStore);

    console.log(data);

  return (
    <div className="container pt-4 pt-md-5">
          {!isLoading ? (
              <div className="row">
              <div className="col-7">
                <h2 style={{color:"#2c5785"}}>Napomena: {data.result[0].napomena}</h2>
                <span>
                  <span
                    className="badge text-bg-dark pt-2"
                    style={{ height: "40px", fontSize: "20px" }}
                  >
                    Tip Postupka: {data.result[0].tipPostupka.naslov}
                  </span>
                </span>
                <span>
                  <span
                    className="badge text-bg-light pt-2"
                    style={{ height: "40px", fontSize: "20px" }}
                  >
                    Broj Sudnice: {data.result[0].brojSudnice}
                  </span>
                </span>
                <p style={{ fontSize: "20px", color:"#2c5785"}} className="pt-2">
                  Lokacija: {data.result[0].lokacija.naslov}
                </p>
                <p style={{ fontSize: "20px", color:"#2c5785"}} className="pt-2">
                  Tip Ustanove: {data.result[0].tipUstanove}
                </p>
                <p style={{ fontSize: "20px", color:"#2c5785"}} className="pt-2">
                  Identifikator Postupka: {data.result[0].identifikatorPostupka}
                </p>
                <p style={{ fontSize: "20px", color:"#2c5785"}} className="pt-2">
                  Sudija: {data.result[0].sudija.ime}
                </p>
                <div className="row pt-4">
                    {userData.role == SD_Roles.ADMIN ? (
                        <div className="col-5">
                            <button className="btn form-control" style={{backgroundColor:"#2c5785", color:"white"}}>
                            Izmeni
                            </button>
                        </div>
                    ) : ""}
                  
        
                  <div className="col-5 ">
                    <button className="btn btn-secondary form-control" onClick={() => navigate("/parnice/parniceLista")}>
                      Nazad
                    </button>
                  </div>
                </div>
              </div>
              <div className='col-5'>
                    <div 
                        className='card'
                        style={{boxShadow:"0 1px 7px 0 rgb(0 0 0 / 50%)"}}>
                        <div className="text-center">
                            <p className="card-title m-0 fs-3" style={{ color: "#2c5785" }}>
                            <label>Informacije o tuženiku: </label>
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Ime: </label>
                                {data.result[0].tuzenik.ime}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Telefon 1: </label>
                                {data.result[0].tuzenik.telefon1}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Telefon 2: </label>
                                {data.result[0].tuzenik.telefon2}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Adresa: </label>
                                {data.result[0].tuzenik.adresa}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Email: </label>
                                {data.result[0].tuzenik.email}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Pravno/Fizičko lice: </label>
                                {data.result[0].tuzenik.pravnoFizickoLice}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Zanimanje: </label>
                                {data.result[0].tuzenik.zanimanje}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Kompanija: </label>
                                {data.result[0].tuzenik.pripadnostKompaniji.naziv}
                            </p>
                        </div>
                    </div>
                    <div 
                        className='card mt-4'
                        style={{boxShadow:"0 1px 7px 0 rgb(0 0 0 / 50%)"}}>
                        <div className="text-center">
                            <p className="card-title m-0 fs-3" style={{ color: "#2c5785" }}>
                            <label>Informacije o tužiocu: </label>
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Ime: </label>
                                {data.result[0].tuzilac.ime}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Telefon 1: </label>
                                {data.result[0].tuzilac.telefon1}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Telefon 2: </label>
                                {data.result[0].tuzilac.telefon2}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Adresa: </label>
                                {data.result[0].tuzilac.adresa}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Email: </label>
                                {data.result[0].tuzilac.email}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Pravno/Fizičko lice: </label>
                                {data.result[0].tuzilac.pravnoFizickoLice}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Zanimanje: </label>
                                {data.result[0].tuzilac.zanimanje}
                            </p>
                            <p className="card-text" style={{ textAlign: "center" }}>
                                <label>Kompanija: </label>
                                {data.result[0].tuzilac.pripadnostKompaniji.naziv}
                            </p>
                        </div>
                    </div>
                </div>
                
            </div>
          ) : (
            <div className = 'd-flex justify-content-center' style={{width: "100%"}}> 
            <MainLoader />
          </div>    
        )}
    
  </div>
  )
}

export default ParnicaDetalji
