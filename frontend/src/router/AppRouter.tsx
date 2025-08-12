import { Routes, Route } from 'react-router-dom';
import Properties from '../features/properties/pages/Properties';
import Owners from '../features/owners/pages/Owners';
import OwnerForm from '../features/owners/pages/OwnerForm';
import PropertyForm from '../features/properties/pages/PropertyForm';
import PropertyDetail from '../features/properties/pages/PropertyDetail';
import PropertyTrace from '../features/properties/pages/PropertyTrace';

const AppRouter = () => {
  return (
    <Routes>
      <Route path="/" element={<Properties />} />
      <Route path="/property/create" element={<PropertyForm />} />
      <Route path='/property/detail/:id' element={<PropertyDetail /> } />
      <Route path='/property/trace/:id' element={<PropertyTrace /> } />

      <Route path="/owners" element={<Owners />} />
      <Route path="/owner/create" element={<OwnerForm />} />
      <Route path="/owner/edit/:id" element={<OwnerForm />} />
    </Routes>
  );
};

export default AppRouter;