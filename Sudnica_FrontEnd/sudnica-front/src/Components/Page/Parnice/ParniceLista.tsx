import React, { useEffect, useState } from 'react'
import { parnicaModel } from '../../../Interfaces'
import { useDispatch } from 'react-redux';
import parniceApi, { useGetParniceQuery } from '../../../apis/parniceApi';
import { setParnica } from '../../../Storage/Redux/parniceSlice';
import ParniceCard from './ParniceCard';
import { MainLoader } from '../Common';

function ParniceLista() {

    const [parnice, setParnice] = useState<parnicaModel[]>([]);
    const dispatch = useDispatch();
    const { data, isLoading } = useGetParniceQuery(null);

    console.log(data);
    
    useEffect(() => {
        if (!isLoading) {
            dispatch(setParnica(data.result));
            setParnice(data.result);
        }
    }, [isLoading]);

    if (isLoading) {
        return <MainLoader />
    }

  return (
    <div className='container p-2'>
        <div className='container row'>
          <div className='my-3'>
              
          </div>
        {parnice.length > 0 && 
            parnice.map((parnica: parnicaModel, index: number) => (
                <ParniceCard parnica = {parnica} key={index} />
            ))}
        </div>
    </div>
      
  )
}

export default ParniceLista
