import React, { useEffect, useState } from 'react'
import { useCreateTipPostupkaMutation, useGetTipPostupkaByIdQuery, useUpdateTipPostupkaMutation } from '../../../apis/tipPostupkaApi';
import { useNavigate, useParams } from 'react-router-dom';
import { inputHelper, toastNotify } from '../../../Helper';
import { MainLoader } from '../Common';

const tipPostupkaData = {
  naslov: ""
};

function AzurirajKreirajTip() {
  const [tipPostupkaInputs, setTipPostupkaInputs] = useState(tipPostupkaData);
    const [loading, setLoading] = useState(false);
    const [createLokacija] = useCreateTipPostupkaMutation();
    const [updateLokacija] = useUpdateTipPostupkaMutation();
    const { id } = useParams();
    const navigate = useNavigate();
    const { data } = useGetTipPostupkaByIdQuery(id);

    useEffect(() => {
        if (data && data.result) {
            const tempData = {
                naslov: data.result.naslov,
            };
            setTipPostupkaInputs(tempData);
        }
    }, [data]);

    console.log(data);

    const handleTipPostupkaInput = (
        e: React.ChangeEvent<
            HTMLInputElement | HTMLSelectElement
        >
    ) => {
        const tempData = inputHelper(e, tipPostupkaInputs);
        setTipPostupkaInputs(tempData);
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        setLoading(true);

        const formData = new FormData();

        formData.append("Naslov", tipPostupkaInputs.naslov ?? "");

        let response;

        if (id) {
            //update
            formData.append("Id", id);
            response = await updateLokacija({ data: formData, id });
            toastNotify("Tip Postupka je uspešno ažuriran!", "success");
        } else {
            //kreiranje
            response = await createLokacija(formData);
            toastNotify("Tip Postupka je uspešno kreiran!", "success");
        }

        if (response) {
            setLoading(false);
            navigate("/tipPostupka/tipPostupkaLista");
        }

        setLoading(false);
    }

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
    <h3 className="px-2" style={{color:"#2c5785"}}>
      {id ? "Ažuriraj Tip Postupka" : "Dodaj novi Tip Postupka"}
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
            value={tipPostupkaInputs.naslov}
            onChange={handleTipPostupkaInput}
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
                  onClick={()=> navigate("/tipPostupka/tipPostupkaLista")}
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

export default AzurirajKreirajTip
