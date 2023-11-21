import React, { useEffect, useState } from 'react'
import { SD_PF_Lice } from '../../../Utility/SD'
import { useCreateContactMutation, useGetKontaktByIdQuery, useUpdateContactMutation } from '../../../apis/kontaktiApi';
import { useNavigate, useParams } from 'react-router-dom';
import { inputHelper, toastNotify } from '../../../Helper';
import { MainLoader } from '../Common';

const PravnoFizickoLice = [
    SD_PF_Lice.Pravno,
    SD_PF_Lice.Fizicko,
];

const kontaktData = {
    ime: "",
    telefon1: "",
    telefon2: "",
    adresa: "",
    email: "",
    pravnoFizickoLice: PravnoFizickoLice[1],
    zanimanje: "",
    kompanijaId: "",
};

function AzurirajKreirajKontakt() {

    const [kontaktiInputs, setKontaktiInputs] = useState(kontaktData);
    const [loading, setLoading] = useState(false);
    const [createKontakt] = useCreateContactMutation();
    const [updateKontakt] = useUpdateContactMutation();
    const { id } = useParams();
    const navigate = useNavigate();
    const { data } = useGetKontaktByIdQuery(id);

    useEffect(() => {
        if (data && data.result) {
            const tempData = {
                ime: data.result.ime,
                telefon1: data.result.telefon1,
                telefon2: data.result.telefon2,
                adresa: data.result.adresa,
                email: data.result.email,
                pravnoFizickoLice: data.result.pravnoFizickoLice,
                zanimanje: data.result.zanimanje,
                kompanijaId: data.result.kompanijaId,
            };
            setKontaktiInputs(tempData);
        }
    }, [data]);

    //console.log(data);

    const handleKontaktInput = (
        e: React.ChangeEvent<
            HTMLInputElement | HTMLSelectElement
        >
    ) => {
        const tempData = inputHelper(e, kontaktiInputs);
        setKontaktiInputs(tempData);
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        setLoading(true);

        const formData = new FormData();

        formData.append("Ime", kontaktiInputs.ime ?? "");
        formData.append("Telefon1", kontaktiInputs.telefon1 ?? "");
        formData.append("Telefon2", kontaktiInputs.telefon2 ?? "");
        formData.append("Adresa", kontaktiInputs.adresa ?? "");
        formData.append("Email", kontaktiInputs.email ?? "");
        formData.append("PravnoFizickoLice", kontaktiInputs.pravnoFizickoLice ?? "");
        formData.append("Zanimanje", kontaktiInputs.zanimanje ?? "");
        formData.append("KompanijaId", kontaktiInputs.kompanijaId ?? "");

        let response;

        if (id) {
            //update
            formData.append("Id", id);
            response = await updateKontakt({ data: formData, id });
            toastNotify("Kontakt je uspešno ažuriran!", "success");
        } else {
            //kreiranje
            response = await createKontakt(formData);
            toastNotify("Kontakt je uspešno kreiran!", "success");
        }

        if (response) {
            setLoading(false);
            navigate("/kontakti/kontaktiLista");
        }

        setLoading(false);
    }

  return (
    <div className="container border mt-5 p-5 bg-light">
      {loading && <MainLoader />}
    <h3 className="px-2" style={{color:"#2c5785"}}>
      {id ? "Ažuriraj kontakt" : "Dodaj novi Kontakt"}
    </h3>
    <form method="post" encType="multipart/form-data" onSubmit={handleSubmit}>
      <div className="row mt-3">
        <div className="col-md">
          <label className='mb-2' style={{color:"black"}}>Ime: </label>
          <input
            type="text"
            className="form-control"
            placeholder="Unesite Ime"
            required
            name='ime'
            value={kontaktiInputs.ime}
            onChange={handleKontaktInput}
          />
          <label className='mt-3' style={{color:"black"}}>Telefon 1: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Telefon 1"
            name='telefon1'
            value={kontaktiInputs.telefon1}
            onChange={handleKontaktInput}
          />
          <label className='mt-3' style={{color:"black"}}>Telefon 2: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Telefon 2"
            name='telefon2'
            value={kontaktiInputs.telefon2}
            onChange={handleKontaktInput}
          />
          <label className='mt-3' style={{color:"black"}}>Adresa: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Adresu"
            name='adresa'
            value={kontaktiInputs.adresa}
            onChange={handleKontaktInput}
          />
          <label className='mt-3' style={{color:"black"}}>Email: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Email"
            name='email'
            value={kontaktiInputs.email}
            onChange={handleKontaktInput}
          />
          <label className='mt-3' style={{color:"black"}}>Pravno/Fizičko Lice: </label>
          <select
            className="form-control mt-3 form-select"
            placeholder="Odaberite između pravnog i fizičkog lica"
            name="pravnoFizickoLice"
            value={kontaktiInputs.pravnoFizickoLice}
            onChange={handleKontaktInput}
          >
            {PravnoFizickoLice.map((pravnoFizickoLice) => (
                <option key={pravnoFizickoLice} 
                    value={pravnoFizickoLice}>
                    {pravnoFizickoLice}
                </option>
            ))}
          </select>
          <label className='mt-3' style={{color:"black"}}>Zanimanje: </label>
          <input
            className="form-control mt-3"
            placeholder="Unesite Zanimanje"
            name='zanimanje'
            value={kontaktiInputs.zanimanje}
            onChange={handleKontaktInput}
          />
          <label className='mt-3' style={{color:"black"}}>ID Kompanije: </label>
          <input
            type="number"
            className="form-control mt-3"
            required
            placeholder="Unesite ID Kompanije"
            name='kompanijaId'
            value={kontaktiInputs.kompanijaId}
            onChange={handleKontaktInput}
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
                  onClick={()=> navigate("/kontakti/kontaktiLista")}
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

export default AzurirajKreirajKontakt
