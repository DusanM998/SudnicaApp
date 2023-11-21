import React, { useEffect, useState } from 'react'
import { useCreateLokacijaMutation, useGetLokacijaByIdQuery, useUpdateLokacijaMutation } from '../../../apis/lokacijeApi';
import { useNavigate, useParams } from 'react-router-dom';
import { inputHelper, toastNotify } from '../../../Helper';
import { MainLoader } from '../Common';

const lokacijaData = {
  naslov: ""
};

function AzurirajKreirajLokacije() {
  const [lokacijeInputs, setLokacijeInputs] = useState(lokacijaData);
    const [loading, setLoading] = useState(false);
    const [createLokacija] = useCreateLokacijaMutation();
    const [updateLokacija] = useUpdateLokacijaMutation();
    const { id } = useParams();
    const navigate = useNavigate();
    const { data } = useGetLokacijaByIdQuery(id);

    useEffect(() => {
        if (data && data.result) {
            const tempData = {
                naslov: data.result.naslov,
            };
            setLokacijeInputs(tempData);
        }
    }, [data]);

    console.log(data);

    const handleLokacijaInput = (
        e: React.ChangeEvent<
            HTMLInputElement | HTMLSelectElement
        >
    ) => {
        const tempData = inputHelper(e, lokacijeInputs);
        setLokacijeInputs(tempData);
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        setLoading(true);

        const formData = new FormData();

        formData.append("Naslov", lokacijeInputs.naslov ?? "");

        let response;

        if (id) {
            //update
            formData.append("Id", id);
            response = await updateLokacija({ data: formData, id });
            toastNotify("Lokacija je uspešno ažurirana!", "success");
        } else {
            //kreiranje
            response = await createLokacija(formData);
            toastNotify("Lokacija je uspešno kreirana!", "success");
        }

        if (response) {
            setLoading(false);
            navigate("/lokacije/lokacijeLista");
        }

        setLoading(false);
    }

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
    <h3 className="px-2" style={{color:"#2c5785"}}>
      {id ? "Ažuriraj lokaciju" : "Dodaj novu Lokaciju"}
    </h3>
    <form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
      <div className="row mt-3">
        <div className="col-md">
          <input
            type="text"
            className="form-control"
            placeholder="Unesite Naslov"
            required
            name='naslov'
            value={lokacijeInputs.naslov}
            onChange={handleLokacijaInput}
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
                  onClick={()=> navigate("/lokacije/lokacijeLista")}
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

export default AzurirajKreirajLokacije
