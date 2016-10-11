using System;

public static class Health {

    public enum state {
        HEALTHY,
        SICKLY,
        DISEASED,
        MORBIDLY_ILL,
        DECEASED
    }

    public static state getInitialState() {
        Random rand = new Random();
        switch (rand.Next(1, 4)) {
            case 1:
                return state.SICKLY;
            case 2:
                return state.DISEASED;
            case 3:
                return state.MORBIDLY_ILL;
            default:
                throw new Exception("This should not happen...");
        }
    }

    public static state nextState(state stateIn) {
        switch(stateIn) {
            case state.HEALTHY:
                return state.SICKLY;
            case state.SICKLY:
                return state.DISEASED;
            case state.DISEASED:
                return state.MORBIDLY_ILL;
            case state.MORBIDLY_ILL:
                return state.DECEASED;
            case state.DECEASED:
                return state.DECEASED;
            default:
                throw new Exception("This should not happen...");
        }
    }

    public static state prevState(state stateIn) {
        switch (stateIn) {
            case state.HEALTHY:
                return state.HEALTHY;
            case state.SICKLY:
                return state.HEALTHY;
            case state.DISEASED:
                return state.SICKLY;
            case state.MORBIDLY_ILL:
                return state.DISEASED;
            case state.DECEASED: // you can never undo a deceased. :P
                return state.DECEASED;
            default:
                throw new Exception("This should not happen...");
        }
    }
}
