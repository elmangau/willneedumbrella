import * as React from 'react';
import { Redirect, Route, RouteProps, RouteComponentProps } from 'react-router';

type ExtendedRouteProps<P> = P extends RouteComponentProps<any>
    ? RouteProps & {
        useAlternatePath?: boolean
        alternatePath?: string
        component: React.ComponentType<P>
        withProps?: Omit<P, keyof RouteComponentProps<any>>
    }
    : never;

export const ExtendedRoute = <P extends RouteComponentProps<any>>({
    component: WrappedComponent,
    withProps,
    ...routeProps
}: ExtendedRouteProps<P>): React.ReactElement<P> => {
    let redirectPath: string = '';
    if (routeProps.useAlternatePath === true && routeProps.alternatePath !== "") {
        redirectPath = routeProps.alternatePath as string;
    }

    if (redirectPath) {
        const renderComponent = () => (<Redirect to={redirectPath} />);
        return <Route {...routeProps} component={renderComponent} render={undefined} />;
    } else {
        return (
            <Route
                {...routeProps}
                render={childProps => {
                    return <WrappedComponent {...childProps} {...withProps} />
                }}
            />
        )
    }
}

export default ExtendedRoute;