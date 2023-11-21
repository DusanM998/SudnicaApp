import React, { useEffect, useState } from 'react';
import { Footer, Header } from '../Components/Layout';
import { Route, Routes } from 'react-router-dom';
import { Description, Home, Login, NotFound, Register } from '../Pages';
import { useDispatch, useSelector } from 'react-redux';
import { userModel } from '../Interfaces';
import { RootState } from '../Storage/Redux/store';
import jwt_decode from "jwt-decode";
import { setLoggedInUser } from '../Storage/Redux/userAuthSlice';
import { AzurirajKreirajKontakt, KontaktiLista } from '../Components/Page/Kontakti';
import { AzurirajKreirajLokacije, LokacijeLista } from '../Components/Page/Lokacije';
import { AzurirajKreirajTip, TipPostupkaLista } from '../Components/Page/TipoviPostupaka';
import { AzurirajKreirajKompaniju, KompanijeLista } from '../Components/Page/Kompanije';
import { AzurirajKreirajParnicu, ParnicaDetalji, ParniceCard, ParniceLista } from '../Components/Page/Parnice';

function App() {

  const [skip, setSkip] = useState(true);
  const dispatch = useDispatch();
  const userData: userModel = useSelector((state: RootState) => state.userAuthStore);
  
  useEffect(() => {
    const localToken = localStorage.getItem("token");

    if (localToken) {
      const { punoIme, id, email, role, godine }: userModel = jwt_decode(localToken);
      dispatch(setLoggedInUser({ punoIme, id, email, role, godine }));
    }
  });

  useEffect(() => {
    if (userData.id) setSkip(false);
  }, [userData]);

  return (
    <div className="text-success">
      <Header />
      <div className='pb-5'>
        <Routes>
          <Route path='/' element={<Home />}></Route>
          <Route path='/login' element={<Login />}></Route>
          <Route path='/register' element={<Register />}></Route>
          <Route path='*' element={<NotFound />}></Route>
          <Route path='/description' element={<Description />} ></Route>
          <Route path='/kontakti/kontaktiLista' element={<KontaktiLista />}></Route>
          <Route path='/kontakti/azurirajKreirajKontakt' element={<AzurirajKreirajKontakt />}></Route>
          <Route path='/kontakti/azurirajKreirajKontakt/:id' element={<AzurirajKreirajKontakt />}></Route>
          <Route path='/lokacije/lokacijeLista' element={<LokacijeLista />} ></Route>
          <Route path='/lokacije/azurirajKreirajLokaciju' element={<AzurirajKreirajLokacije />} ></Route>
          <Route path='/lokacije/azurirajKreirajLokaciju/:id' element={<AzurirajKreirajLokacije />} ></Route>
          <Route path='/tipPostupka/tipPostupkaLista' element={<TipPostupkaLista />} ></Route>
          <Route path='/tipPostupka/azurirajKreirajTip' element={<AzurirajKreirajTip />} ></Route>
          <Route path='/tipPostupka/azurirajKreirajTip/:id' element={<AzurirajKreirajTip />} ></Route>
          <Route path='/kompanije/kompanijeLista' element={<KompanijeLista />} ></Route>
          <Route path='/kompanije/azurirajKreirajKompaniju' element={<AzurirajKreirajKompaniju />} ></Route>
          <Route path='/kompanije/azurirajKreirajKompaniju/:id' element={<AzurirajKreirajKompaniju />} ></Route>
          <Route path='/parnice/parniceLista' element={<ParniceLista />} ></Route>
          <Route path='/parnice/parnicaDetalji/:parnicaId' element={<ParnicaDetalji />} ></Route>
          <Route path='/parnice/azurirajKreirajParnicu' element={<AzurirajKreirajParnicu />}></Route>
          <Route path='/parnice/azurirajKreirajParnicu/:id' element={<AzurirajKreirajParnicu />}></Route>
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;
