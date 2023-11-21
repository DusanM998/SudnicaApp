import React, {useState} from 'react'
import { useRegisterUserMutation } from '../apis/authApi'
import { useNavigate } from 'react-router-dom';
import inputHelper from '../Helper/inputHelper';
import { apiResponse } from '../Interfaces';
import { MainLoader } from '../Components/Page/Common';
import { SD_Roles } from '../Utility/SD';
import { toastNotify } from '../Helper';

function Register() {

  const [registerUser] = useRegisterUserMutation();
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const [userInput, setUserInput] = useState({
    userName: "",
    punoIme: "",
    lozinka: "",
    role: "",
    godine: 0,
  });

  const handleUserInput = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const tempData = inputHelper(e, userInput);
    setUserInput(tempData);
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    const response: apiResponse = await registerUser({
      userName: userInput.userName,
      punoIme: userInput.punoIme,
      lozinka: userInput.lozinka,
      role: userInput.role,
      godine: userInput.godine,
    });
  
    if (response.data) {
      console.log(response.data);
      toastNotify("Uspešna registracija!");
      navigate("/login");
    }
    else if(response.error){
      toastNotify(response.error.data.errorMessages[0], "error");
    }
  
    setLoading(false);
  };
  
  return (
    <div className="container text-center">
      { loading && <MainLoader /> }
    <form method="post" onSubmit={handleSubmit}>
      <h1 className="mt-5" style={{color:"#2c5785"}}>Registruj se</h1>
      <div className="mt-5">
        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
          <input
            type="text"
            className="form-control"
            placeholder="Unesite Korisničko Ime"
            required
            name = "userName"
            value={userInput.userName}
            onChange={handleUserInput}
          />
        </div>
        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
          <input
            type="text"
            className="form-control"
            placeholder="Unesite Ime"
            required
            name = "punoIme"
            value={userInput.punoIme}
            onChange={handleUserInput}
          />
        </div>
        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
          <input
            type="password"
            className="form-control"
            placeholder="Unesite Lozinku"
            required
            name = "lozinka"
            value={userInput.lozinka}
            onChange={handleUserInput}
          />
        </div>
        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
            <select
              className="form-control form-select"
              required
              name="role"
              value={userInput.role}
              onChange={handleUserInput}>
            <option value="">--Izaberi Ulogu--</option>
            <option value={`${SD_Roles.USER}`}>User</option>
            <option value={`${SD_Roles.ADMIN}`}>Admin</option>
          </select>
        </div>
        <div className="col-sm-6 offset-sm-3 col-xs-12 mt-4">
          <input
            type="number"
            className="form-control"
            placeholder="Unesite Godine"
            required
            name = "godine"
            value={userInput.godine}
            onChange={handleUserInput}
          />
        </div>
      </div>
      <div className="mt-5">
        <button type="submit"
          className="btn btn-outlined rounded-pill text-white mx-2"
          style={{ width: "200px", backgroundColor:"#2c5785" }}>
          Registruj se
        </button>
      </div>
    </form>
    </div>
  )
}

export default Register
