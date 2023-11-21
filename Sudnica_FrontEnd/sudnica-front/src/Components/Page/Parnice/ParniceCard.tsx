import React from 'react'
import { apiResponse, parnicaModel, userModel } from '../../../Interfaces'
import { Link, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from '../../../Storage/Redux/store';
import { useGetParniceQuery } from '../../../apis/parniceApi';
import { MainLoader } from '../Common';
import { SD_Roles } from '../../../Utility/SD';

interface Props {
    parnica: parnicaModel;
}

function ParniceCard(props: Props) {

    const navigate = useNavigate();
    const userData : userModel = useSelector((state: RootState) => state.userAuthStore);

    return (  
        <div className='col-md-6 col-14 p-4'>
            <div 
                className='card'
                style={{boxShadow:"0 1px 7px 0 rgb(0 0 0 / 50%)"}}>
                <div className="text-center">
                    <p className="card-title m-0 fs-3" style={{ color: "#2c5785" }}>
                    <label>Napomena: </label>
                    <Link
                        to={`/parnice/parnicaDetalji/${props.parnica.parnicaId}`}
                        style={{textDecoration:"none", color:"#2c5785"}}>
                         {props.parnica.napomena}
                    </Link>
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                    <label>Tip Ustanove: </label>
                        {props.parnica.tipUstanove}
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                        <label>Broj Sudnice: </label>
                        {props.parnica.brojSudnice}
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                        <label>Identifikator Postupka: </label>
                        {props.parnica.identifikatorPostupka}
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                        <label>Tip Postupka: </label>
                        {props.parnica.tipPostupka?.naslov}
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                        <label>Lokacija: </label>
                        {props.parnica.lokacija?.naslov}
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                        <label>Tuženik: </label>
                        {props.parnica.tuzenik?.ime}
                    </p>
                    <p className="card-text" style={{ textAlign: "center" }}>
                        <label>Tužilac: </label>
                        {props.parnica.tuzilac?.ime}
                    </p>
                    {userData.role == SD_Roles.ADMIN || userData.role == SD_Roles.USER ? (
                        <button className="btn m-2"
                            style={{color:"white", backgroundColor:"#2c5785"}}    
                            onClick={() => navigate(`/parnice/parnicaDetalji/${props.parnica.parnicaId}`)}>
                            Detalji
                        </button>
                    ) : ""}
                    
                </div>
                
            </div>
        </div>
    
  )
}

export default ParniceCard
