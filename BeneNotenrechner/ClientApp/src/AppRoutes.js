import { AbsenzenToolView } from "./components/View/AbsenzenToolView";
import { MainView } from "./components/View/MainView";
import { NotenToolView } from "./components/View/NotenToolView";

const AppRoutes = [
    {
        index: true,
        element: <MainView />
    },
    {
        path: '/NotenTool',
        element: <NotenToolView />
    },
];

export default AppRoutes;
