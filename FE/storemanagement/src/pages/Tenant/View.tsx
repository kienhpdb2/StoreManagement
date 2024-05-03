import React, { useEffect, useState } from 'react';
import { Button, Col, Modal, Row, Space, message, Typography  } from 'antd';
import axios from '../../common/baseAxios';
import TenantDto from './models/TenantDto';
import moment from 'moment';
interface IOpenModalProps {
    open: boolean,
    closeForm: Function,
    id: Number
}
const ViewModal: React.FC<IOpenModalProps> = (props: IOpenModalProps) => {
    const { open, closeForm, id } = props;
    const [curentTenant, setCurentTenant] = useState<TenantDto>();

    useEffect(() => {
        getTenantById();
    }, [])
    const getTenantById = () => {
        axios.get(`Tenant/${id}`).then((res) => {
            let { data } = res;
            if (data.success) {
                setCurentTenant(data.data);
            }
            else {
                message.error(data.error);
            }
        })
    }
    const handleDelete = () => {
        axios.delete(`Tenant/${id}`).then((res) => {
            let { data } = res;
            if (data.success) {
                message.success('Xóa cửa hàng thành công.')
                closeForm(true);
            }
            else {
                message.success('Xóa thất bại thất bại.')
            }
        })
    }
    const handleRenderActive = (isActived: boolean) => {
        return (
            <>
                { isActived ? <Typography.Text type="success">Đang Hoạt Động</Typography.Text > : <Typography.Text type="danger">Khóa</Typography.Text>}
            </>
        )
    }
    return (
        <>
            <Modal
                title="CHI TIẾT CỬA HÀNG"
                centered
                open={open}
                onOk={() => closeForm(false)}
                onCancel={() => closeForm(false)}
                footer={null}
                width={700}
            >
                <Row>
                    <Col span={24}>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>
                                Tên Cửa Hàng
                            </Col>
                            <Col span={18}>{curentTenant?.name}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>E-mail:</Col>
                            <Col span={18}>{curentTenant?.email}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Số Điện Thoại:</Col>
                            <Col span={18}>
                                {curentTenant?.phone}
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Địa Chỉ:</Col>
                            <Col span={18}>
                                {curentTenant?.address}
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Người Đại Diện:</Col>
                            <Col span={18}>
                                {curentTenant?.storeOwnerName}
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Lời Giới Thiệu:</Col>
                            <Col span={18}>
                                {curentTenant?.description}
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Trạng Thái:</Col>
                            <Col span={18}>
                                { handleRenderActive(curentTenant?.isActived ?? false)  }
                            </Col>
                        </Row>
                    </Col>
                </Row>
                <Row style={{ marginTop: '15px', textAlign: 'right' }}>
                    <Col span={24}>
                        <Button type="primary" danger onClick={() => handleDelete()}>Xóa</Button>
                    </Col>
                </Row>
            </Modal>
        </>
    );
};

export default ViewModal;
