import React, { useEffect, useState } from 'react'
import { MainLoader } from '../Common'
import { useNavigate, useParams } from 'react-router-dom'
import { useCreateParnicaMutation, useGetParnicaByIdQuery, useUpdateParnicaMutation } from '../../../apis/parniceApi';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { inputHelper, toastNotify } from '../../../Helper';

const parniceData = {
  datumOdrzavanja: new Date(),
  lokacijaId: "",
  sudijaId: "",
  tipUstanove: "",
  identifikatorPostupka: "",
  brojSudnice: "",
  tuzilacId: "",
  tuzenikId: "",
  napomena: "",
  tipPostupkaId: "",
}

function AzurirajKreirajParnicu() {

  const navigate = useNavigate();
  const [parniceInputs, setParniceInputs] = useState(parniceData);
  const [loading, setLoading] = useState(false);
  const [createParnica] = useCreateParnicaMutation();
  const [updateParnica] = useUpdateParnicaMutation();
  const { id } = useParams();
  const { data } = useGetParnicaByIdQuery(id);

  useEffect(() => {
    if (data && data.result) {
      const tempData = {
        datumOdrzavanja: data.result.datumOdrzavanja,
        lokacijaId: data.result.lokacijaId,
        sudijaId: data.result.sudijaId,
        tipUstanove: data.result.tipUstanove,
        identifikatorPostupka: data.result.identifikatorPostupka,
        brojSudnice: data.result.brojSudnice,
        tuzilacId: data.result.tuzilacId,
        tuzenikId: data.result.tuzenikId,
        napomena: data.result.napomena,
        tipPostupkaId: data.result.lokacijaId,
      }
    }
  }, [data]);

  const handleDateChange = (date:Date) => {
    setParniceInputs({
      ...parniceInputs,
      datumOdrzavanja: date,
    });
  };

  const handleParnicaInputs = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const tempData = inputHelper(e, parniceInputs);
    setParniceInputs(tempData);
  };


  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    
    e.preventDefault();

    setLoading(true);

    const formData = new FormData();

    let response;

    formData.append("LokacijaId", parniceInputs.lokacijaId ?? "");
    formData.append("SudijaId", parniceInputs.sudijaId ?? "");
    formData.append("TipUstanove", parniceInputs.tipUstanove ?? "");
    formData.append("IdentifikatorPostupka", parniceInputs.identifikatorPostupka ?? "");
    formData.append("BrojSudnice", parniceInputs.brojSudnice ?? "");
    formData.append("TuzilacId", parniceInputs.tuzilacId ?? "");
    formData.append("TuzenikId", parniceInputs.tuzenikId ?? "");
    formData.append("TuzenikId", parniceInputs.napomena ?? "");
    formData.append("TuzenikId", parniceInputs.tipPostupkaId ?? "");

    if (id) {
      //update
      formData.append("ParnicaId", id);
      response = await updateParnica({ data: formData, id });
      toastNotify("Parnica je uspešno ažurirana!", "success");
    }
    else {
      //create
      response = await createParnica(formData);
      toastNotify("Parnica je uspešno kreirana!", "success");
    }

    if (response) {
      setLoading(false);
      navigate("/parnice/parniceLista");
    }

    setLoading(false);
  }
  

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
    <h3 className="px-2" style={{color:"#2c5785"}}>
      {id ? "Ažuriraj Parnicu" : "Dodaj novu Parnicu"}
    </h3>
    <form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
      <div className="row mt-3">
        <div className="col-md">
          <label className='w-100 mb-2' style={{color:"black"}}>Datum održavanja: </label>
          <DatePicker 
            className='form-control'
            name='datumOdrzavanja'
            selected={parniceInputs.datumOdrzavanja} 
            onChange={handleDateChange}
          />
          <label className='w-100 mt-3' style={{color:"black"}}>Lokacija ID: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite ID Lokacije"
            name='lokacijaId'
            type="number"
            value={parniceInputs.lokacijaId}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Sudija ID: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite ID Sudije"
            name='sudijaId'
            type="number"
            value={parniceInputs.sudijaId}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Tip Ustanove: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Tip Ustanove"
            name='tipUstanove'
            value={parniceInputs.tipUstanove}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Identifikator Postupka: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Identifikator Postupka"
            name='identifikatorPostupka'
            value={parniceInputs.identifikatorPostupka}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Tužilac ID: </label>
          <input
            className="form-control mt-3"
            type="number"
            placeholder="Unestite ID Tužioca"
            name="tuzilacId"
            value={parniceInputs.tuzilacId}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Tuženik ID: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite ID Tuženika"
            name='tuzenikId'
            type="number"
            value={parniceInputs.tuzenikId}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Napomena: </label>
          <input
            className="form-control mt-3"
            required
            placeholder="Unesite Napomenu"
            name='napomena'
            value={parniceInputs.napomena}
            onChange={handleParnicaInputs}
          />
          <label className='mt-3' style={{color:"black"}}>Tip Postupka ID: </label>
          <input
            type="number"
            className="form-control mt-3"
            placeholder="Unesite ID Tipa Postupka"
            name='tipPostupkaId'
            value={parniceInputs.tipPostupkaId}
            onChange={handleParnicaInputs}
          />
            <div className="row">
              <div className="col-6">
                <button
                  type="submit"
                  style={{backgroundColor: "#2c5785", color: "white" }}
                  className="btn mt-5 form-control"
                >
                  {id ? "Ažuriraj" : "Kreiraj"}
                </button>
              </div>
              <div className="col-6">
                <button
                  className="btn btn-secondary mt-5 form-control"
                  onClick={()=> navigate("/parnice/parniceLista")}
                >
                  Nazad
                </button>
              </div>
            </div>
        </div>
      </div>
    </form>
  </div>
  )
}

export default AzurirajKreirajParnicu
