import React from 'react';
import { Banner } from '../Components/Page/Common';
import Description from './Description';

function Home() {
  return (
    <div>
      <Banner />
      <div className='container pt-4'>
        <Description />
      </div>
    </div>
  )
}

export default Home
