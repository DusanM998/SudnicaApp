import React, { useEffect, useState } from 'react'
import { useCreateKompanijaMutation, useGetKompanijaByIdQuery, useUpdateKompanijaMutation } from '../../../apis/kompanijeApi';
import { useNavigate, useParams } from 'react-router-dom';
import { inputHelper, toastNotify } from '../../../Helper';
import { MainLoader } from '../Common';

const kompanijaData = {
    naziv: "",
    adresa: ""
}

function AzurirajKreirajKompaniju() {
    const [kompanijeInputs, setKompanijeInputs] = useState(kompanijaData);
    const [loading, setLoading] = useState(false);
    const [createKompanija] = useCreateKompanijaMutation();
    const [updateKompanija] = useUpdateKompanijaMutation();
    const { id } = useParams();
    const navigate = useNavigate();
    const { data } = useGetKompanijaByIdQuery(id);

    useEffect(() => {
        if (data && data.result) {
            const tempData = {
                naziv: data.result.naziv,
                adresa: data.result.adresa,
            };
            setKompanijeInputs(tempData);
        }
    }, [data]);

    //console.log(data);

    const handleKompanijaInputs = (
        e: React.ChangeEvent<
            HTMLInputElement | HTMLSelectElement
        >
    ) => {
        const tempData = inputHelper(e, kompanijeInputs);
        setKompanijeInputs(tempData);
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        setLoading(true);

        const formData = new FormData();

        formData.append("Naziv", kompanijeInputs.naziv ?? "");
        formData.append("Adresa", kompanijeInputs.adresa ?? "");

        let response;

        if (id) {
            //update
            formData.append("Id", id);
            response = await updateKompanija({ data: formData, id });
            toastNotify("Kompanija je uspešno ažurirana!", "success");
        } else {
            //kreiranje
            response = await createKompanija(formData);
            toastNotify("Kompanija je uspešno kreirana!", "success");
        }

        if (response) {
            setLoading(false);
            navigate("/kompanije/kompanijeLista");
        }

        setLoading(false);
    }

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
    <h3 className="px-2" style={{color:"#2c5785"}}>
      {id ? "Ažuriraj Kompaniju" : "Dodaj novu Kompaniju"}
    </h3>
    <form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
      <div className="row mt-3">
        <div className="col-md">
          <input
            type="text"
            className="form-control"
            placeholder="Unesite Naziv"
            required
            name='naziv'
            value={kompanijeInputs.naziv}
            onChange={handleKompanijaInputs}
          />
          <input
            className="form-control mt-3"
            placeholder="Unesite Adresu"
            name='adresa'
            value={kompanijeInputs.adresa}
            onChange={handleKompanijaInputs}
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
                  onClick={()=> navigate("/kompanije/kompanijeLista")}
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

export default AzurirajKreirajKompaniju
